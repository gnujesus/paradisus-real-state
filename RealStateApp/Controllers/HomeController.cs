using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Models;
using System.Diagnostics;

namespace RealStateApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServiceManager _serviceManager;

        public HomeController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public async Task<IActionResult> Index()
        {
            await _serviceManager.Amenity.Add(new Core.Application.ViewModels.AmenityModels.SaveAmenityViewModel() { Name = "Brahiam", Description = "My desc" });
            var list = await _serviceManager.Amenity.GetAllViewModel();

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }

            return View();
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
