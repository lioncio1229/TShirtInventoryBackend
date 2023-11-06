using Microsoft.AspNetCore.Mvc;
using TshirtInventoryBackend.Models;
using TshirtInventoryBackend.Repositories;


namespace TshirtInventoryBackend.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserRepository _userRepo;
        private readonly RoleRepository _roleRepo;

        public UsersController(UserRepository repository, RoleRepository roleRepo)
        {
            _userRepo = repository;
            _roleRepo = roleRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return await _userRepo.GetAll();
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<User>> Get([FromQuery] string email)
        {
            var user = await _userRepo.Get(email);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Register(UserRegistrationInputs user)
        {
            var newUser = new User
            {
                Email = user.Email,
                Password = user.Password,
                FullName = user.FullName,
                Role = null,
                IsActived = true,
            };
            await _userRepo.Add(newUser);
            return CreatedAtAction(nameof(Get), new { email = user.Email }, user);
        }

        [HttpPut("{email}")]
        public async Task<IActionResult> Put(string email, User user)
        {
            if (email != user.Email)
            {
                return BadRequest();
            }

            await _userRepo.Update(user);
            return NoContent();
        }

        [HttpPatch("{email}/role")]
        public async Task<IActionResult> Patch(string email, int roleId)
        {
            var role = await _roleRepo.Get(roleId);
            var user = await _userRepo.Get(email);

            if(user == null)
            {
                return NotFound();
            }

            user.Role = role;
            await _userRepo.Update(user);

            return Ok(user);
        }


        [HttpDelete("email")]
        public async Task<ActionResult<User>> Delete(string email)
        {
            var user = await _userRepo.Delete(email);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }
    }
}
