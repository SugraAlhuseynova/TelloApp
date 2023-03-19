using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tello.Service.Client.Member.IServices;

namespace Tello.Api.Apps.Member.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Member")]
    public class BrandsController : ControllerBase
    {
        readonly IBrandService _brandService;
        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            return Ok(_brandService.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _brandService.GetAsync(id));
        }
    }
}
