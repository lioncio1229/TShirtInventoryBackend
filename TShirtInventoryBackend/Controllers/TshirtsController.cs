using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TshirtInventoryBackend.DTOs;
using TshirtInventoryBackend.Models;
using TshirtInventoryBackend.Models.Request;
using TshirtInventoryBackend.Repositories.Interface;

namespace TshirtInventoryBackend.Controllers
{
    [Route("api/v1/tshirt")]
    [ApiController]
    public class TshirtsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TshirtsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TshirtDTO>>> GetAll()
        {
            var tshirts = await _unitOfWork.TshirtRepository.GetAll();
            return Ok(tshirts.Select(tshirt => _mapper.Map<TshirtDTO>(tshirt)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TshirtDTO>> Get(int id)
        {
            var tshirt = await _unitOfWork.TshirtRepository.Get(id);
            if(tshirt == null) 
            {
                return NotFound();
            }
            return Ok(_mapper.Map<TshirtDTO>(tshirt));
        }

        [HttpPost]
        public async Task<ActionResult<TshirtDTO>> Add(TshirtRequest tshirt)
        {
            var newTshirt = await _unitOfWork.AddTshirt(tshirt);
            return CreatedAtAction(nameof(Get), new { id = newTshirt.Id}, _mapper.Map<TshirtDTO>(newTshirt));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TshirtRequest tshirt)
        {
            var tshirtToUpdate = await _unitOfWork.TshirtRepository.Get(id);
            if(tshirtToUpdate == null)
            {
                return NotFound();
            }

            if(tshirtToUpdate.Id != id)
            {
                return BadRequest();
            }

            await _unitOfWork.UpdateTshirt(id, tshirt);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TshirtDTO>> Delete(int id)
        {
            var tshirtToRemove = await _unitOfWork.RemoveTshirt(id);

            if(tshirtToRemove == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<TshirtDTO>(tshirtToRemove));
        }

        [HttpPatch("{id}/quantity")]
        public async Task<IActionResult> UpdateQuantity(int id, TshirtUpdateQuantityRequest updateRequest)
        {
            var tshirt = await _unitOfWork.TshirtRepository.Get(id);
            if (tshirt == null)
            {
                return NotFound();
            }
            tshirt.QuantityInStock = updateRequest.Quantity;
            _unitOfWork.Complete();

            return NoContent();
        }
    }
}
