using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Tello.Api.Test.DTOs;
using Tello.Api.Test.DTOs.Brand;

namespace Tello.Api.Test.Controllers
{
    public class BrandsController : Controller
    {
        string endpoint = null;

        public async Task<IActionResult> Index(int page)
        {
            HttpResponseMessage response = null;
            if (page == 0)
                page = 1;
            endpoint = "https://localhost:7067/api/admin/brands/all/"+page;


            using (HttpClient client = new HttpClient())
            {
                if (Request.Cookies["AuthToken"] != null)
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer "+Request.Cookies["AuthToken"]);
                }
                response = await client.GetAsync(endpoint);
            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                PaginatedListDto<BrandListItemGetDto> items = JsonConvert.DeserializeObject<PaginatedListDto<BrandListItemGetDto>>(content);
                return View(items);
            }
            return RedirectToAction("login", "users");
        }

        public async Task<IActionResult> Edit(int id)
        {
            HttpResponseMessage response = null;
            endpoint = "https://localhost:7067/api/admin/brands/" + id;


            using (HttpClient client = new HttpClient())
            {
                if (Request.Cookies["AuthToken"] != null)
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Request.Cookies["AuthToken"]);
                }
                response = await client.GetAsync(endpoint);
            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                BrandGetDto item = JsonConvert.DeserializeObject<BrandGetDto>(content);
                BrandPostDto dto = new BrandPostDto
                {
                    Name = item.Name
                };
                return View(dto);
            }
            return RedirectToAction("Error", "home");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, BrandPostDto postDto)
        {
            if (!ModelState.IsValid)
                return View();
            HttpResponseMessage response= null;
            endpoint = "https://localhost:7067/api/admin/brands/" + id;
            var requestContent = new StringContent(JsonConvert.SerializeObject(postDto), Encoding.UTF8, "application/json");
            using(HttpClient client = new HttpClient())
            {
                if (Request.Cookies["AuthToken"] != null)
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Request.Cookies["AuthToken"]);
                }
                response = await client.PutAsync(endpoint, requestContent);
            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("index");
            }
            else if(response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                ViewBag.Error = response.StatusCode;
                return View(postDto);
            }
            return RedirectToAction("error", "home");
        }
        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage responseMessage= null;
            endpoint = "https://localhost:7067/api/admin/brands/" + id;
            using (HttpClient client = new HttpClient())
            {
                if (Request.Cookies["AuthToken"] != null)
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Request.Cookies["AuthToken"]);
                }
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
            endpoint = "https://localhost:7067/api/admin/brands";
            StringContent content = new StringContent(JsonConvert.SerializeObject(postDto), Encoding.UTF8, "application/json");
            using (HttpClient client = new HttpClient())
            {
                if (Request.Cookies["AuthToken"] != null)
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Request.Cookies["AuthToken"]);
                }
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
            endpoint = "https://localhost:7067/api/admin/brands/all/deleted" + page;


            using (HttpClient client = new HttpClient())
            {
                if (Request.Cookies["AuthToken"] != null)
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Request.Cookies["AuthToken"]);
                }
                response = await client.GetAsync(endpoint);
            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                PaginatedListDto<BrandListItemGetDto> items = JsonConvert.DeserializeObject<PaginatedListDto<BrandListItemGetDto>>(content);
                return View(items);
            }
            return RedirectToAction("Error", "home");
        }
        public async Task<IActionResult> Restore(int id)
        {
            HttpResponseMessage responseMessage = null;
            endpoint = "https://localhost:7067/api/admin/brands/restore/" + id;
            using (HttpClient client = new HttpClient())
            {
                if (Request.Cookies["AuthToken"] != null)
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Request.Cookies["AuthToken"]);
                }
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
