using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TshirtInventoryBackend.DTOs;
using TshirtInventoryBackend.Models;
using TshirtInventoryBackend.Models.Reponse;
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
        public async Task<IActionResult> Authenticate(UserInputCredentials credentials)
        {
            var user = await _unitOfWork.UserRepositories.GetUserWithEmailAndPassword(credentials.Email, credentials.Password);
            if(user == null)
            {
                return Unauthorized();
            }

            var token = _unitOfWork.GenerateToken(user.Email, user.Role.Name);
            return Ok(new TokenResponse { Token = token });
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
            var token = _unitOfWork.GenerateToken(user.Email, user.Role.Name);

            return Ok(new TokenResponse { Token = token });
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if(identity != null)
            {
                var jtiClaim = identity.FindFirst(JwtRegisteredClaimNames.Jti);

                if(jtiClaim != null)
                {
                    var revokedToken = jtiClaim.Value;
                    _unitOfWork.BlacklistToken(revokedToken);

                    return Ok(new { message = "Successfully Logged Out" });
                }
            }

            return BadRequest();
        }
    }
}
