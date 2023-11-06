using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TshirtInventoryBackend.DTOs;
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
        private readonly IMapper _mapper;

        public UsersController(UserRepository repository, RoleRepository roleRepo, IMapper mapper)
        {
            _userRepo = repository;
            _roleRepo = roleRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> Get()
        {
            var users = await _userRepo.GetAll();
            var mappedUsers = users.Select(user => _mapper.Map<UserDTO>(user));
            return Ok(mappedUsers);
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<UserDTO>> Get(string email)
        {
            var user = await _userRepo.Get(email);
            if (user == null)
            {
                return NotFound();
            }
            return _mapper.Map<UserDTO>(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> Add(UserRegistrationInputs user)
        {
            var role = await _roleRepo.Get(user.RoleId);

            var newUser = new User
            {
                Email = user.Email,
                Password = user.Password,
                FullName = user.FullName,
                Role = role,
                IsActived = true,
            };
            await _userRepo.Add(newUser);
            return CreatedAtAction(nameof(Get), new { email = user.Email }, _mapper.Map<UserDTO>(newUser));
        }

        [HttpPut("{email}")]
        public async Task<IActionResult> Put(string email, UserUpdateInputs user)
        {
            var userToUpdate = await _userRepo.Get(email);
            if (userToUpdate == null)
            {
                return NotFound();
            }

            userToUpdate.FullName = user.FullName;
            userToUpdate.Role = await _roleRepo.Get(user.RoleId);
            userToUpdate.IsActived = user.IsActive;

            await _userRepo.Update(userToUpdate);
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

            return Ok(_mapper.Map<UserDTO>(user));
        }


        [HttpDelete("{email}")]
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
