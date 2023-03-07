using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Runtime.CompilerServices;
using System.Text;
using Tello.Api.Test.DTOs.User;

namespace Tello.Api.Test.Controllers
{
    public class UsersController : Controller
    {
        HttpResponseMessage response = new();
        string endpoint = string.Empty;
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
                    return RedirectToAction("index", "home");
                }
            }
            return RedirectToAction("error", "home");
        }
    }
}
