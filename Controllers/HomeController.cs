using Microsoft.AspNetCore.Mvc;

namespace HRSystem.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
