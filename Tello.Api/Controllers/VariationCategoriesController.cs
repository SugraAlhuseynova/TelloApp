using Microsoft.AspNetCore.Mvc;
using Tello.Core.Entities;
using Tello.Core.IRepositories;
using Tello.Service.Apps.Admin.DTOs.VariationCategoryDTOs;
using Tello.Service.Apps.Admin.IServices;


namespace Tello.Api.Controllers
{
    [Route("api/admin/variarionCategory")]
    [ApiController]
    public class VariationCategoriesController : ControllerBase
    {
        private readonly IVariationCategoryService _variationCategoryService;

        public VariationCategoriesController(IVariationCategoryService variationCategoryService)
        {
            _variationCategoryService = variationCategoryService;
        }
        //GET: api/<VariationCategoriesController>
        [HttpGet]
        public IActionResult GetAll(int page)
        {
            return Ok (_variationCategoryService.GetAll(page));
        }
        [HttpGet("deleted")]
        public IActionResult GetAllDeleted(int page)
        {
            return Ok(_variationCategoryService.GetAllDeleted(page));
        }
        //GET api/<VariationCategoriesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _variationCategoryService.GetAsync(id));
        }

        //POST api/<VariationCategoriesController>
        [HttpPost]
        public async Task Post([FromBody] VariationCategoryPostDto postDto)
        {
            await _variationCategoryService.CreateAsync(postDto);
        }

        // PUT api/<VariationCategoriesController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] VariationCategoryPostDto postDto)
        {
            await _variationCategoryService.UpdateAsync(id, postDto);
        }

        // DELETE api/<VariationCategoriesController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _variationCategoryService.Delete(id);
        }
        [HttpPut("{id}/restore")]
        public async Task Restore(int id)
        {
            await _variationCategoryService.Restore(id);
        }
    }
}
