using Microsoft.AspNetCore.Mvc;
using Tello.Service.Apps.Admin.DTOs.ProductDTOs;
using Tello.Service.Apps.Admin.IServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tello.Api.Apps.Admin.Controllers
{
    [Route("api/admin/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        // GET: api/<ProductsController>
        [HttpGet]
        public IActionResult GetAll(int page)
        {
            return Ok(_productService.GetAll(page));
        }
        [HttpGet("deleted")]
        public IActionResult GetAllDeleted(int page)
        {
            return Ok(_productService.GetAllDeleted(page));
        }
        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _productService.GetAsync(id));
        }

        // POST api/<ProductsController>
        [HttpPost]
        public async Task Post([FromBody] ProductPostDto postDto)
        {
            await _productService.CreateAsync(postDto);
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] ProductPostDto postDto)
        {
            await _productService.UpdateAsync(id, postDto);
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _productService.Delete(id);
        }
        [HttpPut("{id}/restore")]
        public async Task Restore(int id)
        {
            await _productService.Restore(id);
        }
    }
}
