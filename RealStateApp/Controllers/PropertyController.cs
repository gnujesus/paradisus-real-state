using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Models;
using System.Diagnostics;

namespace RealStateApp.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IServiceManager _serviceManager;

        public PropertyController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public async Task<IActionResult> Index()
        {
            var properties = await _serviceManager.Property.GetAllViewModel();
            return View(properties);
        }

        public async Task<IActionResult> Favorites()
        {
            var favorites = await _serviceManager.Favorite.GetAllViewModel();
            return View(favorites);
        }

        public async Task<IActionResult> Single(string id)
        {
            var favorite = await _serviceManager.Property.GetByIdSaveViewModel(id);
            return View(favorite);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

