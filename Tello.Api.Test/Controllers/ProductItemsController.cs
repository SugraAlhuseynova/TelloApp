using Microsoft.AspNetCore.Mvc;

namespace Tello.Api.Test.Controllers
{
    public class ProductItemsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
