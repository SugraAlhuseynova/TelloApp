using Microsoft.AspNetCore.Mvc;
using Tello.Api.Helpers;
using Tello.Service.Apps.Admin.DTOs.SlideDTOs;
using Tello.Service.Apps.Admin.IServices.IEntityServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tello.Api.Apps.Admin.Controllers
{
    [Route("api/admin/slides")]
    [ApiController]
    public class SlidesController : ControllerBase
    {
        private readonly ISlideService _slideService;
        private readonly IWebHostEnvironment _web;

        public SlidesController(ISlideService slideService, IWebHostEnvironment web)
        {
            _slideService = slideService;
            _web = web;

        }
        // GET: api/<SlideController>
        [HttpGet("all/{page}")]
        public IActionResult GetAll(int page)
        {
            return Ok(_slideService.GetAll(page));
        }
        [HttpGet("all/deleted/{page}")]
        public IActionResult GetAllDeleted(int page)
        {
            return Ok(_slideService.GetAllDeleted(page));
        }
        // GET api/<SlideController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _slideService.GetByIdAsync(id));
        }

        // POST api/<SlideController>
        [HttpPost]
        public async Task Post([FromForm] SlidePostDto postDto)
        {
            await _slideService.CreateAsync(postDto);
        }

        [HttpPut("{id}")]
        public async Task Put(int id, [FromForm] SlidePostDto postDto)
        {
            await _slideService.UpdateAsync(id, postDto);
        }

        // DELETE api/<SlideController>/5
        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id)
        {
            await _slideService.DeleteAsync(id);
        }
        [HttpPut("restore/{id}")]
        public async Task RestoreAsync(int id)
        {
            await _slideService.Restore(id);
        }
    }
}
