using Microsoft.AspNetCore.Mvc;
using TshirtInventoryBackend.Models;
using TshirtInventoryBackend.Repositories;
using TshirtInventoryBackend.Repositories.Common;

namespace TshirtInventoryBackend.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        protected readonly UserRepository _repository;

        public UsersController(UserRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return await _repository.GetAll();
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<User>> Get([FromQuery] string email)
        {
            var user = await _repository.Get(email);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(RegistrationCredentials user)
        {
            var newUser = new User
            {
                Email = user.Email,
                Password = user.Password,
                FullName = user.FullName,
                Role = null,
                IsActived = true,
            };
            await _repository.Add(newUser);
            return CreatedAtAction(nameof(Get), new { email = user.Email }, user);
        }

        [HttpPut("{email}")]
        public async Task<IActionResult> Put([FromQuery] string email, User user)
        {
            if (email != user.Email)
            {
                return BadRequest();
            }

            await _repository.Update(user);
            return NoContent();
        }

        [HttpDelete("email")]
        public async Task<ActionResult<User>> Delete(string email)
        {
            var user = await _repository.Delete(email);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }
    }
}
