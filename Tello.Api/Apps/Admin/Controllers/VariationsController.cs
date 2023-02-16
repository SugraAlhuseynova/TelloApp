using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.OpenApi.Writers;
using Tello.Service.Apps.Admin.DTOs.VariationDTOs;
using Tello.Service.Apps.Admin.IServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tello.Api.Apps.Admin.Controllers
{
    [Route("api/admin/variation")]
    [ApiController]
    public class VariationsController : ControllerBase
    {
        private readonly IVariationService _variationService;
        public VariationsController(IVariationService variationService)
        {
            _variationService = variationService;
        }
        // GET: api/<VariationsController>
        [HttpGet]
        public async Task<IActionResult> GetAll(int page)
        {
            return Ok(_variationService.GetAll(page));
        }
        [HttpGet("deleted")]
        public async Task<IActionResult> GetAllDeleted(int page)
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
        [HttpPut("{id}/deleted")]
        public async Task RestoreAsync(int id)
        {
            await _variationService.Restore(id);
        }
    }
}
