using Microsoft.AspNetCore.Mvc;
using TshirtInventoryBackend.Models.Request;
using TshirtInventoryBackend.Repositories.Interface;

namespace TshirtInventoryBackend.Controllers
{
    [Route("api/v1/customer")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrdersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("{id}/order")]
        public async Task<IActionResult> CreateOrder(int id, OrderRequest orderRequest)
        {
            bool isCreated = await _unitOfWork.CreateOrder(id, orderRequest);
            if (!isCreated)
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}
