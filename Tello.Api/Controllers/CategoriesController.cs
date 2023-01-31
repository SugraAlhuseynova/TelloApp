using Microsoft.AspNetCore.Mvc;
using Tello.Core.IRepositories;
using Tello.Service.Apps.Admin.DTOs.CategoryDTOs;
using Tello.Service.Apps.Admin.IServices;

namespace Tello.Api.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("id")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _categoryService.GetAsync(id));
        }
        [HttpGet("all")]
        public IActionResult GetAll(int page)
        {
            return Ok(_categoryService.GetAll(page));
        }
        [HttpPost("")]
        public async Task Create(CategoryPostDto postDto)
        {
            await _categoryService.CreateAsync(postDto);
        }
        [HttpDelete("id")]
        public async Task Delete(int id)
        {
            await _categoryService.Delete(id);
        }
        [HttpPut("id")]
        public async Task Update(int id, CategoryPostDto categoryPostDto)
        {
            await _categoryService.UpdateAsync(id, categoryPostDto);
        }
    }
}
