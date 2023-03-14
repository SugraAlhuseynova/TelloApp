using Microsoft.AspNetCore.Mvc;
using Tello.Core.Entities;
using Tello.Core.IRepositories;
using Tello.Service.Apps.Admin.DTOs.VariationCategoryDTOs;
using Tello.Service.Apps.Admin.IServices.IEntityServices;

namespace Tello.Api.Apps.Admin.Controllers
{
    [Route("api/admin/variationcategories")]
    [ApiController]
    public class VariationCategoriesController : ControllerBase
    {
        private readonly IVariationCategoryService _variationCategoryService;

        public VariationCategoriesController(IVariationCategoryService variationCategoryService)
        {
            _variationCategoryService = variationCategoryService;
        }
        //GET: api/<VariationCategoriesController>
        [HttpGet("all/{page}")]
        public IActionResult GetAll(int page = 1)
        {
            return Ok(_variationCategoryService.GetAll(page));
        }
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            return Ok(_variationCategoryService.GetAll1());
        }
        [HttpGet("all/deleted/{page}")]
        public IActionResult GetAllDeleted(int page = 1)
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
        [HttpDelete("restore/{id}")]
        public async Task Restore(int id)
        {
            await _variationCategoryService.Restore(id);
        }
    }
}
