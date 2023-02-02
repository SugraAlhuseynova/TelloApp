﻿using Microsoft.AspNetCore.Mvc;
using Tello.Service.Apps.Admin.DTOs.VariationOptionDTOs;
using Tello.Service.Apps.Admin.IServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tello.Api.Controllers
{
    [Route("api/variationOption")]
    [ApiController]
    public class VariationOptionController : ControllerBase
    {
        private readonly IVariationOptionService _variationOptionService;

        public VariationOptionController(IVariationOptionService variationOptionService)
        {
            _variationOptionService = variationOptionService;
        }
        // GET: api/<VariationOptionController>
        [HttpGet("all")]
        public IActionResult Get(int page)
        {
            return Ok(_variationOptionService.GetAll(page));
        }
        [HttpGet("all/deleted")]
        public IActionResult GetDeleted(int page)
        {
            return Ok(_variationOptionService.GetAllDeleted(page));
        }
        // GET api/<VariationOptionController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            return Ok(await _variationOptionService.GetAsync(id));
        }
 
        // POST api/<VariationOptionController>
        [HttpPost]
        public async Task Post([FromBody] VariationOptionPostDto postDto)
        {
            await _variationOptionService.CreateAsync(postDto);
        }

        // PUT api/<VariationOptionController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] VariationOptionPostDto postDto)
        {
            await _variationOptionService.UpdateAsync(id, postDto);
        }

        // DELETE api/<VariationOptionController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _variationOptionService.Delete(id);
        }
        [HttpPut("{id}/restore")]
        public async Task Restore(int id)
        {
            await _variationOptionService.Restore(id);
        }
    }
}
