using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.ViewModels.PropertyModels;
using RealStateApp.Models;
using System.Diagnostics;

namespace RealStateApp.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View(new List<PropertyViewModel>());
        }
    }
}
