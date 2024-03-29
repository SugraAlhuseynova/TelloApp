﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tello.Service.Apps.Admin.DTOs.BrandDTOs;
using Tello.Service.Apps.Admin.IServices.IEntityServices;

namespace Tello.Api.Apps.Admin.Controllers
{
    [Route("api/admin/brands")]
    [ApiController]
    [Authorize(Roles = "Superadmin")]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_brandService.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _brandService.GetAsync(id));
        }
        [HttpGet("all/{page}")]
        public IActionResult GetAll(int page = 1)
        {
            var data = _brandService.GetAll(page);
            return Ok(data);
        }
        [HttpGet("all/deleted{page}")]
        public IActionResult GetAllDeleted(int page)
        {
            return Ok(_brandService.GetAllDeleted(page));
        }
        [HttpPost("")]
        public async Task Create(BrandPostDto postDto)
        {
            await _brandService.CreateAsync(postDto);
        }
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _brandService.Delete(id);
        }
        [HttpDelete("restore/{id}")]
        public async Task Restore(int id)
        {
            await _brandService.Restore(id);
        }
        [HttpPut("{id}")]
        public async Task Update(int id, BrandPostDto PostDto)
        {
            await _brandService.UpdateAsync(id, PostDto);
        }
    }
}
