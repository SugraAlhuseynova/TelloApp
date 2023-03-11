using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Tello.Api.Test.DTOs;
using Tello.Api.Test.DTOs.Comment;

namespace Tello.Api.Test.Controllers
{
    public class CommentsController : Controller
    {
        string endpoint = null;
        HttpResponseMessage response = new(); 
        public async Task<IActionResult> Index(int page)
        {
            if (page == 0)
                page = 1;
            endpoint = "https://localhost:7067/api/admin/comments/all/" + page;
            using(HttpClient client = new HttpClient())
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
                PaginatedListDto<CommentListItemDto> items = JsonConvert.DeserializeObject<PaginatedListDto<CommentListItemDto>>(content);
                return View(items);
            }
            return RedirectToAction("error", "home");
        }
        public async Task<IActionResult> GetAllDeleted(int page)
        {
            if (page == 0)
                page = 1;
            endpoint = "https://localhost:7067/api/admin/comments/all/deleted/" + page;
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
                PaginatedListDto<CommentListItemDto> items = JsonConvert.DeserializeObject<PaginatedListDto<CommentListItemDto>>(content);
                return View(items);
            }
            return RedirectToAction("error", "home");
        }
        public async Task<IActionResult> Delete(int id)
        {
            endpoint = "https://localhost:7067/api/admin/comments/" + id;
            using (HttpClient client = new HttpClient())
            {
                if (Request.Cookies["AuthToken"] != null)
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Request.Cookies["AuthToken"]);
                }
                response = await client.DeleteAsync(endpoint);
            }
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("error", "home");
        }
        public async Task<IActionResult> Detail(int id)
        {
            endpoint = "https://localhost:7067/api/admin/comments/" + id;
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
                CommentGetDto item = JsonConvert.DeserializeObject<CommentGetDto>(content);
                return View(item);
            }
            return RedirectToAction("error", "home");
        }

    }
}
