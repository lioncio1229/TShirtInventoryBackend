using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TshirtInventoryBackend.Models;
using TshirtInventoryBackend.Repositories.Interface;

namespace TshirtInventoryBackend.Controllers
{
    [Route("/api/v1/tshirtorders")]
    [ApiController]
    public class TshirtOrdersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public TshirtOrdersController(IUnitOfWork unitOfWork)
        { 
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerator<TshirtOrder>>> GetAll()
        {
            var tshirtOrders = await _unitOfWork.TshirtOrderRepository.GetAllAsync();
            return Ok(tshirtOrders);
        }
    }
}
