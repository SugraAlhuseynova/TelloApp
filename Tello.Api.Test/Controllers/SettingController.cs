using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;
using Tello.Api.Test.Models;

namespace Tello.Api.Test.Controllers
{
    public class SettingController : Controller
    {
        string endpoint = null;
        public async Task<IActionResult> Index(int page)
        {
            HttpResponseMessage responseMessage = null;
            if (page == 0)
                page = 1;

            endpoint = "https://localhost:7067/api/admin/settings/all/"+page;
            using(HttpClient client = new HttpClient())
            {
                responseMessage = await client.GetAsync(endpoint);
            }
            if (responseMessage!=null && responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await responseMessage.Content.ReadAsStringAsync();
                SettingListGetDto<SettingGetDto> items = JsonConvert.DeserializeObject<SettingListGetDto<SettingGetDto>>(content);
                return View(items);
            }
            return RedirectToAction("error", "home");
        }
        public async Task<IActionResult> Edit(int id)
        {
            HttpResponseMessage responseMessage = null;
            endpoint = "https://localhost:7067/api/admin/settings/"+id;
            using (HttpClient client = new HttpClient())
            {
                responseMessage = await client.GetAsync(endpoint);
            }
            if (responseMessage != null && responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await responseMessage.Content.ReadAsStringAsync();
                SettingGetDto settingGetDto = JsonConvert.DeserializeObject<SettingGetDto>(content);
                SettingPostDto postDto = new SettingPostDto { Key = settingGetDto.Key, Value = settingGetDto.Value };
                return View(postDto);
            }
            return RedirectToAction("error", "home");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, SettingPostDto postDto)
        {
            HttpResponseMessage responseMessage = null;
            endpoint = "https://localhost:7067/api/admin/settings/" + id;

            var content = new StringContent(JsonConvert.SerializeObject(postDto), Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                responseMessage = await client.PutAsync(endpoint, content);
            }
            if (responseMessage!=null && responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("index");
            }
            return RedirectToAction("error", "home");
        }


    }
}
