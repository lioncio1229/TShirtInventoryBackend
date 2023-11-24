using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TshirtInventoryBackend.Models.Reponse;
using TshirtInventoryBackend.Repositories.Interface;

namespace TshirtInventoryBackend.Controllers
{
    [Route("api/v1/analytics")]
    [ApiController]
    public class Analytics : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Analytics(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("summary")]
        public async Task<ActionResult<SummaryAnalytics>> GetSummaryAnalytics()
        {
            var summaryAnalytics = await _unitOfWork.GetSummaryAnalytics();
            return Ok(summaryAnalytics);
        }

        [HttpGet("topproducts")]
        public async Task<ActionResult<IEnumerable<TopProductItem>>> GetTopProducts([FromQuery] int takeCount=5)
        {
            var topProducts = await _unitOfWork.GetTopProducts(takeCount);
            return Ok(topProducts);
        }
    }
}
