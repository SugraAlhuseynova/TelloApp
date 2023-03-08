using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using Tello.Api.Test.DTOs.User;

namespace Tello.Api.Test.Controllers
{
    public class UsersController : Controller
    {
        HttpResponseMessage response = new();
        string endpoint = string.Empty;
        
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            endpoint = "https://localhost:7067/api/admin/users/login";
            StringContent requestContent = new StringContent(JsonConvert.SerializeObject(loginDto), Encoding.UTF8, "application/json");
            using(HttpClient client = new HttpClient())
            {
                response = await client.PostAsync(endpoint, requestContent);
            }
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                UserLoginResponseDto responseToken = JsonConvert.DeserializeObject<UserLoginResponseDto>(content);
                if (responseToken.Token != null)
                {
                    Response.Cookies.Append("AuthToken", responseToken.Token);
                    return RedirectToAction("index", "brands");
                }
            }
            return RedirectToAction("error", "home");
        }
        public async Task<IActionResult> Logout()
        {
            if (Request.Cookies["AuthToken"] != null)
            {
                Response.Cookies.Delete("AuthToken");
                return RedirectToAction(nameof(Login));
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }
        public async Task<IActionResult> CreateAdmin()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAdmin(UserPostDto postDto)
        {
            endpoint = "https://localhost:7067/api/admin/users/admin";
            StringContent content = new StringContent(JsonConvert.SerializeObject(postDto),Encoding.UTF8, "application/json");
            using(HttpClient client = new HttpClient())
            {
                if (Request.Cookies["AuthToken"] != null)
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Request.Cookies["AuthToken"]);
                }
                response = await client.PostAsync(endpoint, content);
            }
            if (response!=null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("error", "home");
        }


    }

}
