using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Tello.Api.Test.DTOs;
using Tello.Api.Test.DTOs.Slide;

namespace Tello.Api.Test.Controllers
{
    public class SlidesController : Controller
    {
        string endpoint = null;
        HttpResponseMessage response = new();
        public async Task<IActionResult> Index(int page)
        {
            HttpResponseMessage response = null;
            if (page == 0)
                page = 1;
            endpoint = "https://localhost:7067/api/admin/slides/all/" + page;


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
                PaginatedListDto<SlideListItemGetDto> items = JsonConvert.DeserializeObject<PaginatedListDto<SlideListItemGetDto>>(content);
                return View(items);
            }
            return RedirectToAction("login", "users");


        }

    }
}
