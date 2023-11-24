using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TshirtInventoryBackend.DTOs;
using TshirtInventoryBackend.Models;
using TshirtInventoryBackend.Models.Reponse;
using TshirtInventoryBackend.Models.Request;
using TshirtInventoryBackend.Repositories.Interface;

namespace TshirtInventoryBackend.Controllers
{
    [Route("/api/v1/tshirtorders")]
    [ApiController]
    public class TshirtOrdersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TshirtOrdersController(IUnitOfWork unitOfWork, IMapper mapper)
        { 
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerator<TshirtOrderDTO>>> GetAll()
        {
            var tshirtOrders = await _unitOfWork.TshirtOrderRepository.GetAllAsync();
            return Ok(tshirtOrders.Select(to => _mapper.Map<TshirtOrderDTO>(to)));
        }

        [HttpGet("q")]
        public async Task<ActionResult<IEnumerator<TshirtOrderDTO>>> GetAllWithQuery(string searchByProductId = "", int statusId=5)
        {
            IEnumerable<TshirtOrder> tshirtOrders;

            if(statusId == 5)
            {
                tshirtOrders = await _unitOfWork.TshirtOrderRepository.GetAllWithQuery(searchByProductId);
            }
            else
            {
                tshirtOrders = await _unitOfWork.TshirtOrderRepository.GetAllWithQuery(searchByProductId, statusId);
            }

            return Ok(tshirtOrders.Select(to => _mapper.Map<TshirtOrderDTO>(to)));
        }

        [HttpPut("{productId}/status")]
        public async Task<IActionResult> UpdateStatus(string productId, UpdateOrderStatusRequest updateOrderStatusRequest)
        {
            var tshirtOrder = _unitOfWork.TshirtOrderRepository.Find(to => to.ProductId == productId).FirstOrDefault();
            var status = _unitOfWork.StatusRepository.Get(updateOrderStatusRequest.StatusId);

            if(tshirtOrder == null || status == null)
            {
                return NotFound();
            }
            _unitOfWork.UpdateTshirtOrderStatus(tshirtOrder, status);

            return NoContent();
        }

        [HttpGet("salesummary")]
        public ActionResult<SaleSummeryResponse> GetSaleSummary()
        {
            return Ok(_unitOfWork.TshirtOrderRepository.GetSaleSummary());
        }
    }
}
