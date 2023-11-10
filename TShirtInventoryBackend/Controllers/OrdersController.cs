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
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetAll()
        {
            var orders = await _unitOfWork.OrderRepository.GetAllAsync();
            return Ok(orders.Select(order => _mapper.Map<OrderDTO>(order)));
        }

        [HttpGet("order/{orderId}")]
        public async Task<ActionResult<OrderDTO>> Get(int orderId)
        {
            var order = await _unitOfWork.OrderRepository.GetAsync(orderId);
            return Ok(_mapper.Map<OrderDTO>(order));
        }

        [HttpPost("{id}/order")]
        public async Task<ActionResult<OrderDTO>> CreateOrder(int id, OrderRequest orderRequest)
        {
            var customer = await _unitOfWork.CustomerRepository.GetAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            var result = await _unitOfWork.CreateOrder(id, orderRequest);
            if(result == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Get), new {orderId = result.Id}, _mapper.Map<OrderDTO>(result));
        }
    }
}
