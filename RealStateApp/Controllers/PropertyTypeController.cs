using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.ViewModels.PropertyModels;
using RealStateApp.Core.Application.ViewModels.TypePropertyModels;

namespace RealStateApp.Controllers
{
    public class PropertyTypeController : Controller
    {
        private readonly IServiceManager _serviceManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PropertyTypeController(IServiceManager serviceManager, IHttpContextAccessor httpContextAccessor)
        {
            _serviceManager = serviceManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            List<TypePropertyViewModel> vmList = new();
            var trackChanges = true;
            vmList = await _serviceManager.TypeProperty.GetAllViewModel(new List<string> { "Properties" }, trackChanges);

            return View(vmList);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await _serviceManager.TypeProperty.Delete(id);
            return RedirectToRoute(new {Controller="PropertyType", Action="Index"});
        }

        public IActionResult Save()
        {
            return View(new SaveTypePropertyViewModel());
        }

        [HttpPost]
        public IActionResult Save(SaveTypePropertyViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //vm.Properties = null;
                _serviceManager.TypeProperty.Add(vm);
                return RedirectToAction("Index");
            }
            return View(vm);
        }
    }
}

