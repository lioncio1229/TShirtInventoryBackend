using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TshirtInventoryBackend.Models;
using TshirtInventoryBackend.Repositories;

namespace TshirtInventoryBackend.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public AuthController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IActionResult Authenticate(UserInputCredentials credentials)
        {
            var token = _unitOfWork.Authenticate(credentials.Email, credentials.Password);
            if(token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }
    }
}
