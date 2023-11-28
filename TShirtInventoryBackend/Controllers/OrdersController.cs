using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TshirtInventoryBackend.DTOs;
using TshirtInventoryBackend.Models;
using TshirtInventoryBackend.Models.Request;
using TshirtInventoryBackend.Repositories.Interface;

namespace TshirtInventoryBackend.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;

        public OrdersController(IUnitOfWork unitOfWork, IMapper mapper, IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailSender = emailSender;
        }

        [HttpGet("order")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetAll()
        {
            var orders = await _unitOfWork.OrderRepository.GetAllAsync();
            return Ok(orders.Select(order => _mapper.Map<OrderDTO>(order)));
        }

        [HttpGet("order/{id}")]
        public async Task<ActionResult<OrderDTO>> Get(int id)
        {
            var order = await _unitOfWork.OrderRepository.GetAsync(id);
            return Ok(_mapper.Map<OrderDTO>(order));
        }

        [HttpPost("customer/{id}/order")]
        public async Task<ActionResult<OrderDTO>> CreateOrder(int id, OrderRequest orderRequest)
        {
            var customer = await _unitOfWork.CustomerRepository.GetAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            try
            {
                var result = await _unitOfWork.CreateOrder(customer, orderRequest);
                if(result == null)
                {
                    return BadRequest();
                }

                var tshirts = orderRequest.TshirtRequests.Select(to => _unitOfWork.TshirtRepository.Get(to.TshirtId));
                await SendEmailIfProductsAreLessthanAmount(tshirts);

                return CreatedAtAction(nameof(Get), new {id = result.Id}, _mapper.Map<OrderDTO>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [NonAction]
        public async Task SendEmailIfProductsAreLessthanAmount(IEnumerable<Tshirt> tshirts, int amount = 5)
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync();

            foreach (var tshirt in tshirts)
            {
                if(tshirt.QuantityInStock < amount)
                {
                    _emailSender.SendEmail(users, tshirt.Name, tshirt.QuantityInStock);
                }
            }
        }

        //[HttpPatch("order/{id}")]
        //public async Task<IActionResult> UpdateOrderStatus(int id, UpdateOrderStatusRequest updateOrderStatusRequest)
        //{
        //    bool isSuccess = await _unitOfWork.UpdateOrderStatus(id, updateOrderStatusRequest.StatusId);
        //    if (isSuccess)
        //    {
        //        return Ok();
        //    }
        //    return NotFound();
        //}
    }
}
