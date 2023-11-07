using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TshirtInventoryBackend.DTOs;
using TshirtInventoryBackend.Models;
using TshirtInventoryBackend.Repositories;

namespace TshirtInventoryBackend.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuthController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(UserInputCredentials credentials)
        {
            var token = _unitOfWork.Authenticate(credentials.Email, credentials.Password);
            if(token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegistrationCredentials credentials)
        {
            var newUser = new UserAddInputs
            {
                Email = credentials.Email,
                Password = credentials.Password,
                FullName = credentials.FullName,
                RoleId = 4
            };

            var user = await _unitOfWork.AddNewUser(newUser);

            return Ok(_mapper.Map<UserDTO>(user));
        }
    }
}
