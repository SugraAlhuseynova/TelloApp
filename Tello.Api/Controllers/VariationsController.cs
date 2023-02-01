using Microsoft.AspNetCore.Mvc;
using Tello.Service.Apps.Admin.DTOs.VariationDTOs;
using Tello.Service.Apps.Admin.IServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tello.Api.Controllers
{
    [Route("api/variation")]
    [ApiController]
    public class VariationsController : ControllerBase
    {
        private readonly IVariationService _variationService;
        public VariationsController(IVariationService variationService)
        {
            _variationService = variationService;
        }
        // GET: api/<VariationsController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<VariationsController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<VariationsController>
        [HttpPost]
        public async Task CreateAsync([FromBody] VariationPostDto variationPostDto)
        {
            await _variationService.CreateAsync(variationPostDto);
        }

        // PUT api/<VariationsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] VariationPostDto postDto)
        {
           return Ok(_variationService.UpdateAsync(id, postDto));
        }

        //// DELETE api/<VariationsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
