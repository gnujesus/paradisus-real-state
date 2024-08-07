using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.ViewModels.AmenityModels;

namespace RealStateApp.Controllers
{
    public class AmenityController : Controller
    {
        private readonly IServiceManager _serviceManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AmenityController(IServiceManager serviceManager, IHttpContextAccessor httpContextAccessor)
        {
            _serviceManager = serviceManager;
            _httpContextAccessor = httpContextAccessor;
        }

        [Authorize(Roles = "Admin, Agent, Developer")]
        public async Task<IActionResult> Index()
        {
            List<AmenityViewModel> vmList = await _serviceManager.Amenity.GetAllViewModel();
            return View(vmList);
        }

        [HttpPost]
        public async Task<IActionResult> Save(string id)
        {
            await _serviceManager.Amenity.Delete(id);
            return RedirectToRoute(new {Controller="Amenity", Action="Index"});
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await _serviceManager.Amenity.Delete(id);
            return RedirectToRoute(new { Controller = "Amenity", Action = "Index" });
        }

    }
}
