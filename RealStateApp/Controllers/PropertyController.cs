using Microsoft.AspNetCore.Mvc;

namespace RealStateApp.Controllers
{
    public class PropertyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
