using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TshirtInventoryBackend.DTOs;
using TshirtInventoryBackend.Models;
using TshirtInventoryBackend.Models.Reponse;
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
        private readonly IWebHostEnvironment _environment;

        public TshirtsController(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment environment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _environment = environment;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tshirt>>> GetAll()
        {
            var tshirts = await _unitOfWork.TshirtRepository.GetAllAsync();
            var tshirtsDTOs = new List<TshirtDTO>();
            if(tshirts != null && tshirts.Count() > 0)
            {
                foreach (var item in tshirts)
                {
                    var tshirtItem = _mapper.Map<TshirtDTO>(item);
                    tshirtItem.ProductImageUrl = GetImagebyProduct(item.Id.ToString());
                    tshirtsDTOs.Add(tshirtItem);
                }
            }
            return Ok(tshirtsDTOs);
        }

        [HttpGet("q")]
        public async Task<ActionResult<TshirtsResponse>> GetAllWithQuery([FromQuery] int skipRows, [FromQuery] int numberOfItems, string searchByName = "")
        {
            var tshirts = await _unitOfWork.TshirtRepository.GetWithQuery(skipRows, numberOfItems, searchByName);
            var tshirtsDTOs = new List<TshirtDTO>();
            if (tshirts != null && tshirts.Count() > 0)
            {
                foreach (var item in tshirts)
                {
                    var tshirtItem = _mapper.Map<TshirtDTO>(item);
                    tshirtItem.ProductImageUrl = GetImagebyProduct(item.Id.ToString());
                    tshirtsDTOs.Add(tshirtItem);
                }
            }
            var result = new TshirtsResponse
            {
                Total = tshirts.Count(),
                tshirts = tshirtsDTOs
            };

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TshirtDTO>> Get(int id)
        {
            var tshirt = await _unitOfWork.TshirtRepository.GetAsync(id);
            if(tshirt == null) 
            {
                return NotFound();
            }
            var tshirtDTO = _mapper.Map<TshirtDTO>(tshirt);
            tshirtDTO.ProductImageUrl = GetImagebyProduct(tshirt.Id.ToString());

            return Ok(tshirtDTO);
        }

        [HttpPost]
        public async Task<ActionResult<Tshirt>> Add(TshirtRequest tshirt)
        {
            var newTshirt = await _unitOfWork.AddTshirt(tshirt);
            return CreatedAtAction(nameof(Get), new { id = newTshirt.Id}, newTshirt);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TshirtRequest tshirt)
        {
            var tshirtToUpdate = await _unitOfWork.TshirtRepository.GetAsync(id);
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
        public async Task<ActionResult<Tshirt>> Delete(int id)
        {
            var tshirtToRemove = await _unitOfWork.RemoveTshirt(id);

            if(tshirtToRemove == null)
            {
                return NotFound();
            }

            return Ok(tshirtToRemove);
        }

        [HttpPatch("{id}/quantity")]
        public async Task<IActionResult> UpdateQuantity(int id, TshirtUpdateQuantityRequest updateRequest)
        {
            var tshirt = await _unitOfWork.TshirtRepository.GetAsync(id);
            if (tshirt == null)
            {
                return NotFound();
            }
            tshirt.QuantityInStock = updateRequest.Quantity;
            _unitOfWork.Complete();

            return NoContent();
        }

        [HttpPost("image")]
        public async Task<ActionResult> UploadImage()
        {
            bool Results = false;
            try
            {
                var _uploadedfiles = Request.Form.Files;
                foreach (IFormFile source in _uploadedfiles)
                {
                    string Filename = source.FileName;
                    string Filepath = GetFilePath(Filename);

                    if (!Directory.Exists(Filepath))
                    {
                        Directory.CreateDirectory(Filepath);
                    }

                    string imagepath = Filepath + "\\image.png";

                    if (System.IO.File.Exists(imagepath))
                    {
                        System.IO.File.Delete(imagepath);
                    }
                    using (FileStream stream = System.IO.File.Create(imagepath))
                    {
                        await source.CopyToAsync(stream);
                        Results = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Ok(Results);
        }

        [HttpDelete("image/{id}")]
        public IActionResult RemoveImage(string id)
        {
            string Filepath = GetFilePath(id);
            string Imagepath = Filepath + "\\image.png";
            try
            {
                if (System.IO.File.Exists(Imagepath))
                {
                    System.IO.File.Delete(Imagepath);
                }
                return Ok();
            }
            catch (Exception ext)
            {
                throw ext;
            }
        }

        [NonAction]
        private string GetFilePath(string ProductCode)
        {
            return this._environment.WebRootPath + "\\Uploads\\Product\\" + ProductCode;
        }

        [NonAction]
        private string GetImagebyProduct(string productcode)
        {
            string ImageUrl = string.Empty;
            string HostUrl = "https://localhost:7297/";
            string Filepath = GetFilePath(productcode);
            string Imagepath = Filepath + "\\image.png";
            if (!System.IO.File.Exists(Imagepath))
            {
                ImageUrl = HostUrl + "/uploads/common/noimage.jpg";
            }
            else
            {
                ImageUrl = HostUrl + "/uploads/Product/" + productcode + "/image.png";
            }
            return ImageUrl;
        }
    }
}
