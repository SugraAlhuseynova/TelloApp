using Microsoft.AspNetCore.Mvc;
using Tello.Service.Apps.Admin.DTOs.BrandDTOs;
using Tello.Service.Apps.Admin.IServices;

namespace Tello.Api.Controllers
{
    [Route("api/brands")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }
        [HttpGet("id")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _brandService.GetAsync(id));
        }
        [HttpGet("all")]
        public IActionResult GetAll(int page)
        {
            return Ok(_brandService.GetAll(page));
        }
        [HttpPost("")]
        public async Task Create(BrandPostDto postDto)
        {
            await _brandService.CreateAsync(postDto);
        }
        [HttpDelete("id")]
        public async Task Delete(int id)
        {
            await _brandService.Delete(id);
        }
        [HttpPut("id")]
        public async Task Update(int id, BrandPostDto PostDto)
        {
            await _brandService.UpdateAsync(id, PostDto);
        }
    }
}
