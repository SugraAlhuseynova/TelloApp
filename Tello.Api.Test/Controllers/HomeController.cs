using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Tello.Api.Test.Models;

namespace Tello.Api.Test.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            using(HttpClient client = new HttpClient())
            {
                if (Request.Cookies["AuthToken"] != null)
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " +Request.Cookies["AuthToken"]);
                    return View();
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