using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TshirtInventoryBackend.Controllers
{
    [Authorize]
    [Route("api/v1/tshirt")]
    [ApiController]
    public class TshirtsController : ControllerBase
    {

    }
}
