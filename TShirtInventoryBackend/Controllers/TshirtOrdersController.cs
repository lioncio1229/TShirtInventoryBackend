using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TshirtInventoryBackend.DTOs;
using TshirtInventoryBackend.Models;
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
    }
}
