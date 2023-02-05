using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Signing;
using Tello.Service.Apps.Admin.DTOs.ProductItemVariationDTOs;
using Tello.Service.Apps.Admin.IServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tello.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductItemVariationsController : ControllerBase
    {
        private readonly IProductItemVariationService _productItemVariationService;

        public ProductItemVariationsController(IProductItemVariationService productItemVariationService)
        {
            _productItemVariationService = productItemVariationService;
        }
        // GET: api/<ProductConfigurationController>
        [HttpGet]
        public IActionResult GetAll(int page)
        {
            return Ok(_productItemVariationService.GetAll(page));
        }
        [HttpGet("all/deleted")]
        public IActionResult GetAllDeleted(int page)
        {
            return Ok(_productItemVariationService.GetAllDeleted(page));
        }
        // GET api/<ProductConfigurationController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _productItemVariationService.GetAsync(id));
        }

        // POST api/<ProductConfigurationController>
        [HttpPost]
        public async Task Post([FromBody] ProductItemVariationPostDto postDto)
        {
            await _productItemVariationService.CreateAsync(postDto);
        }

        // PUT api/<ProductConfigurationController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] ProductItemVariationPostDto postDto)
        {
            await _productItemVariationService.UpdateAsync(id, postDto);
        }

        // DELETE api/<ProductConfigurationController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _productItemVariationService.Delete(id);
        }
        [HttpPut("{id}/deleted")]
        public async Task Restore(int id)
        {
            await _productItemVariationService.Restore(id);
        }
    }
}
