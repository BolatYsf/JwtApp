using Microsoft.AspNetCore.Mvc;

namespace JwtApp.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
