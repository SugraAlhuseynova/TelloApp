using Microsoft.AspNetCore.Mvc;
using Tello.Service.Apps.Admin.DTOs.VariationDTOs;
using Tello.Service.Apps.Admin.IServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tello.Api.Apps.Admin.Controllers
{
    [Route("api/admin/variations")]
    [ApiController]
    public class VariationsController : ControllerBase
    {
        private readonly IVariationService _variationService;
        public VariationsController(IVariationService variationService)
        {
            _variationService = variationService;
        }
        // GET: api/<VariationsController>
        [HttpGet("all")]
        public async Task<IActionResult> GetAll1()
        {
            return Ok(_variationService.GetAll1());
        }
        [HttpGet("all/{page}")]
        public async Task<IActionResult> GetAll(int page = 1) 
        {
            return Ok(_variationService.GetAll(page));
        }
        [HttpGet("all/deleted/{page}")]
        public async Task<IActionResult> GetAllDeleted(int page = 1)
        {
            return Ok(_variationService.GetAllDeleted(page));
        }
        // GET api/<VariationsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _variationService.GetAsync(id));
        }

        // POST api/<VariationsController>
        [HttpPost]
        public async Task CreateAsync([FromBody] VariationPostDto variationPostDto)
        {
            await _variationService.CreateAsync(variationPostDto);
        }

        // PUT api/<VariationsController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] VariationPostDto postDto)
        {
            await _variationService.UpdateAsync(id, postDto);
        }

        // DELETE api/<VariationsController>/5
        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id)
        {
            await _variationService.Delete(id);
        }
        [HttpDelete("restore/{id}")]
        public async Task RestoreAsync(int id)
        {
            await _variationService.Restore(id);
        }
    }
}
