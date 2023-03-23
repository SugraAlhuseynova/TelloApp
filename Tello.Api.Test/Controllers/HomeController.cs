using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System.Diagnostics;
using Tello.Api.Test.DTOs.Card;
using Tello.Api.Test.Models;
using Tello.Api.Test.ViewModels.Home;

namespace Tello.Api.Test.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        string endpoint;
        HttpResponseMessage response = new();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            using (HttpClient client = new HttpClient())
            {
                if (Request.Cookies["AuthToken"] != null)
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Request.Cookies["AuthToken"]);
                    endpoint = "https://localhost:7067/api/admin/cards/all";
                    response = await client.GetAsync(endpoint);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        var cardsDto = JsonConvert.DeserializeObject<List<CardGetDto>>(content);
                        HomeViewModel homeVM = new HomeViewModel()
                        {
                            Cards = cardsDto
                        };

                        return View(homeVM);
                    }
                    else
                    {
                        RedirectToAction(nameof(Error));
                    }


                }
            }
            return RedirectToAction("login", "users");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}