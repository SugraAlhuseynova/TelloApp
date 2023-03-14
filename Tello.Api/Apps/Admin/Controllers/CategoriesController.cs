using Microsoft.AspNetCore.Mvc;
using Tello.Service.Apps.Admin.DTOs.CategoryDTOs;
using Tello.Service.Apps.Admin.IServices.IEntityServices;

namespace Tello.Api.Apps.Admin.Controllers
{
    [ApiController]
    [Route("api/admin/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            return Ok(_categoryService.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _categoryService.GetAsync(id));
        }
        [HttpGet("all/{page}")]
        public IActionResult GetAll(int page = 1)
        {
            return Ok(_categoryService.GetAll(page));
        }
        [HttpGet("all/deleted/{page}")]
        public IActionResult GetAllDeleted(int page = 1)
        {
            return Ok(_categoryService.GetAllDeleted(page));
        }
        [HttpDelete("restore/{id}")]
        public async Task Restore(int id)
        {
            await _categoryService.Restore(id);
        }
        [HttpPost("")]
        public async Task Create(CategoryPostDto postDto)
        {
            await _categoryService.CreateAsync(postDto);
        }
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _categoryService.Delete(id);
        }
        [HttpPut("{id}")]
        public async Task Update(int id, CategoryPostDto categoryPostDto)
        {
            await _categoryService.UpdateAsync(id, categoryPostDto);
        }
    }
}
