using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using NuGet.Repositories;
using System.Text;
using Tello.Api.Test.Models;

namespace Tello.Api.Test.Controllers
{
    public class BrandController : Controller
    {
        public async Task<IActionResult> Index(int page)
        {
            HttpResponseMessage response = null;
            if (page == 0)
                page = 1;
            string endpoint = "https://localhost:7067/api/admin/brands/all/"+page;


            using (HttpClient client = new HttpClient())
            {
                response = await client.GetAsync(endpoint);
            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                BrandListGetDto<BrandItemGetDto> items = JsonConvert.DeserializeObject<BrandListGetDto<BrandItemGetDto>>(content);
                return View(items);
            }
            return RedirectToAction("Error", "home");
        }

        public async Task<IActionResult> Edit(int id)
        {
            HttpResponseMessage response = null;
            string endpoint = "https://localhost:7067/api/admin/brands/" + id;


            using (HttpClient client = new HttpClient())
            {
                response = await client.GetAsync(endpoint);
            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                BrandItemGetDto item = JsonConvert.DeserializeObject<BrandItemGetDto>(content);
                return View(item);
            }
            return RedirectToAction("Error", "home");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, BrandPostDto postDto)
        {
            HttpResponseMessage response= null;
            string endpoint = "https://localhost:7067/api/admin/brands/"+id;
            var requestContent = new StringContent(JsonConvert.SerializeObject(postDto), Encoding.UTF8, "application/json");
            using(HttpClient client = new HttpClient())
            {
                response = await client.PutAsync(endpoint, requestContent);
            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("index");
            }
            return NotFound();
        }
    }
}
