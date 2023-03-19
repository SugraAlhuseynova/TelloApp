using Microsoft.AspNetCore.Mvc;
using Tello.Service.Client.Member.IServices;

namespace Tello.Api.Apps.Member.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductItemController : ControllerBase
    {
        private readonly IProductItemService _productItemService;

        public ProductItemController(IProductItemService productItemService)
        {
            _productItemService = productItemService;
        }
        [HttpGet]
        public IActionResult GetAll(int page = 1)
        {
            return Ok(_productItemService.GetAll(page));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _productItemService.GetAsync(id));
        }
    }
}
