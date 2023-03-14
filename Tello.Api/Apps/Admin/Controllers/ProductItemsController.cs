using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Writers;
using Tello.Service.Apps.Admin.DTOs.ProductItemDTOs;
using Tello.Service.Apps.Admin.IServices.IEntityServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tello.Api.Apps.Admin.Controllers
{
    [Route("api/admin/productitems")]
    [ApiController]
    public class ProductItemsController : ControllerBase
    {
        private readonly IProductItemService _productItemService;

        public ProductItemsController(IProductItemService productItemService)
        {
            _productItemService = productItemService;
        }
        // GET: api/<ProductItemController>
        [HttpGet("all/{page}")]
        public IActionResult GetAll(int page)
        {
            return Ok(_productItemService.GetAll(page));
        }
        [HttpGet("all/deleted/{page}")]
        public IActionResult GetDeleted(int page)
        {
            return Ok(_productItemService.GetAllDeleted(page));
        }
        // GET api/<ProductItemController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _productItemService.GetAsync(id));
        }
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            return Ok(_productItemService.GetAll());
        }

        // POST api/<ProductItemController>
        [HttpPost]
        public async Task Post([FromBody] ProductItemPostDto postDto)
        {
            await _productItemService.CreateAsync(postDto);
        }

        // PUT api/<ProductItemController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] ProductItemPostDto postDto)
        {
            await _productItemService.UpdateAsync(id, postDto);
        }

        // DELETE api/<ProductItemController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _productItemService.Delete(id);
        }
        [HttpDelete("restore/{id}")]
        public async Task Restore(int id)
        {
            await _productItemService.Restore(id);
        }
    }
}
