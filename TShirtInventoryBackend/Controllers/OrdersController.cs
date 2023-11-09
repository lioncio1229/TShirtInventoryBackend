using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TshirtInventoryBackend.DTOs;
using TshirtInventoryBackend.Models.Request;
using TshirtInventoryBackend.Repositories.Interface;

namespace TshirtInventoryBackend.Controllers
{
    [Route("api/v1/customer")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrdersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("order")]
        public async Task<ActionResult<OrderDTO>> GetAll()
        {
            var orders = await _unitOfWork.OrderRepository.GetAllAsync();
            return Ok(orders.Select(order => _mapper.Map<OrderDTO>(order)));
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
