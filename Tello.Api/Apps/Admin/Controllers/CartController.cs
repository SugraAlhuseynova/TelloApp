using Microsoft.AspNetCore.Mvc;
using Tello.Service.Apps.Admin.IServices.IEntityServices;

namespace Tello.Api.Apps.Admin.Controllers
{
    [Route("api/admin/cards")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cardService;

        public CartController(ICartService cardService)
        {
            _cardService = cardService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _cardService.GetAsync(id));
        }
        [HttpGet("all/{page}")]
        public IActionResult GetAll(int page = 1)
        {
            return Ok(_cardService.GetAll(page));
        }
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            return Ok(_cardService.GetAll());
        }
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] byte status)
        {
            await _cardService.ChangeStatus(id, status);
        }

    }
}
