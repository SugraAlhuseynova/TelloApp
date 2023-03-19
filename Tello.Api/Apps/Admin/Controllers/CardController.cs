using Microsoft.AspNetCore.Mvc;
using Tello.Service.Apps.Admin.IServices.IEntityServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tello.Api.Apps.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }
        // GET api/<CardController>/5
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
        // PUT api/<CardController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] byte status)
        {
            await _cardService.ChangeStatus(id, status);
        }

    }
}
