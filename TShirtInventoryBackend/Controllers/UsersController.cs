using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TshirtInventoryBackend.DTOs;
using TshirtInventoryBackend.Models;
using TshirtInventoryBackend.Repositories;

namespace TshirtInventoryBackend.Controllers
{
    [Authorize]
    [Route("api/v1/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public UsersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userRepo = unitOfWork.UserRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            var users = await _userRepo.GetAll();
            return Ok(users.Select(user => _mapper.Map<UserDTO>(user)));
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> Get(string email)
        {
            var user = await _userRepo.GetUserWithEmail(email);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<UserDTO>(user));
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserAddInputs userInputs)
        {
            var newUser = await _unitOfWork.AddNewUser(userInputs);
            return CreatedAtAction(nameof(Get), new { email = userInputs.Email }, _mapper.Map<UserDTO>(newUser));
        }

        [HttpPut("{email}")]
        public async Task<IActionResult> Update(string email, UserUpdateInputs inputs)
        {
            var updatedUser = await _unitOfWork.UpdateUser(email, inputs);
            if(updatedUser == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{email}")]
        public async Task<IActionResult> Delete(string email)
        {
            var user = await _unitOfWork.UserRepository.RemoveWithEmail(email);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<UserDTO>(user));
        }
    }
}
