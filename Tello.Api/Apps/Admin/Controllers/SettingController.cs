using Microsoft.AspNetCore.Mvc;
using Tello.Service.Apps.Admin.DTOs.SettingDTOs;
using Tello.Service.Apps.Admin.IServices.IEntityServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tello.Api.Apps.Admin.Controllers
{
    [Route("api/admin/settings")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        private readonly ISettingService _settingService;

        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }
        // GET: api/<SettingController>
        [HttpGet("all/{page}")]
        public IActionResult GetAll(int page = 1)
        {
            return Ok(_settingService.GetAll(page));
        }

        // GET api/<SettingController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _settingService.GetAsync(id));
        }

        // POST api/<SettingController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        // PUT api/<SettingController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, SettingPostDto postDto)
        {
            await _settingService.UpdateAsync(id, postDto);
        }

        // DELETE api/<SettingController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _settingService.Delete(id);
        }
    }
}
