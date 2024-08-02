using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.ViewModels.UserModels;

namespace RealStateApp.Controllers
{
    public class AgentController : Controller
    {
        public IActionResult Index()
        {
            return View(new List<UserViewModel>());
        }
    }
}
