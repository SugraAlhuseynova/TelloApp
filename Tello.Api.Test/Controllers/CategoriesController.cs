using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Tello.Api.Test.DTOs;
using Tello.Api.Test.DTOs.Brand;
using Tello.Api.Test.DTOs.Category;

namespace Tello.Api.Test.Controllers
{
    public class CategoriesController : Controller
    {
        string endpoint = null;
        public async Task<IActionResult> Index(int page)
        {
            HttpResponseMessage response = null;
            if (page == 0)
                page = 1;

            endpoint = "https://localhost:7067/api/admin/categories/all/" + page;
            using (var client = new HttpClient())
            {
                response = await client.GetAsync(endpoint);
            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                PaginatedListDto<CategoryListItemDto> items = JsonConvert.DeserializeObject<PaginatedListDto<CategoryListItemDto>>(content);
                return View(items);
            }
            return RedirectToAction("error", "home");
        }
        public async Task<IActionResult> Edit(int id)
        {
            HttpResponseMessage response = null;
            endpoint = "https://localhost:7067/api/admin/categories/" + id;


            using (HttpClient client = new HttpClient())
            {
                response = await client.GetAsync(endpoint);
            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                CategoryGetDto item = JsonConvert.DeserializeObject<CategoryGetDto>(content);
                CategoryPostDto dto = new CategoryPostDto
                {
                    Name = item.Name
                };
                return View(dto);
            }
            return RedirectToAction("Error", "home");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, CategoryPostDto postDto)
        {
            HttpResponseMessage response = null;
            endpoint = "https://localhost:7067/api/admin/categories/" + id;
            var requestContent = new StringContent(JsonConvert.SerializeObject(postDto), Encoding.UTF8, "application/json");
            using (HttpClient client = new HttpClient())
            {
                response = await client.PutAsync(endpoint, requestContent);
            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("index");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                ViewBag.Error = response.StatusCode;
                return View(postDto);
            }
            return RedirectToAction("error", "home");
        }
        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage responseMessage = null;
            endpoint = "https://localhost:7067/api/admin/categories/" + id;
            using (HttpClient client = new HttpClient())
            {
                responseMessage = await client.DeleteAsync(endpoint);
            }
            if (responseMessage != null && responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok();
            }
            return NotFound();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(BrandPostDto postDto)
        {
            if (!ModelState.IsValid)
                return View();
            HttpResponseMessage responseMessage = null;
            endpoint = "https://localhost:7067/api/admin/categories";
            StringContent content = new StringContent(JsonConvert.SerializeObject(postDto), Encoding.UTF8, "application/json");
            using (HttpClient client = new HttpClient())
            {
                responseMessage = await client.PostAsync(endpoint, content);
            }
            if (responseMessage != null && responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("index");
            }
            return View();
        }
        public async Task<IActionResult> GetAllDeleted(int page)
        {
            HttpResponseMessage response = null;
            if (page == 0)
                page = 1;
            endpoint = "https://localhost:7067/api/admin/categories/all/deleted/" + page;


            using (HttpClient client = new HttpClient())
            {
                response = await client.GetAsync(endpoint);
            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                PaginatedListDto<CategoryListItemDto> items = JsonConvert.DeserializeObject<PaginatedListDto<CategoryListItemDto>>(content);
                return View(items);
            }
            return RedirectToAction("Error", "home");
        }
        public async Task<IActionResult> Restore(int id)
        {
            HttpResponseMessage responseMessage = null;
            endpoint = "https://localhost:7067/api/admin/categories/restore/" + id;
            using (HttpClient client = new HttpClient())
            {
                responseMessage = await client.DeleteAsync(endpoint);
            }
            if (responseMessage != null && responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
