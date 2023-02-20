using Microsoft.AspNetCore.Mvc;

namespace Tello.Api.Test.Controllers
{
    public class SettingController : Controller
    {
        string endpoint = null;
        public IActionResult Index()
        {
            //HttpResponseMessage responseMessage = null;
            //endpoint = "";


            return View();
        }
    }
}
