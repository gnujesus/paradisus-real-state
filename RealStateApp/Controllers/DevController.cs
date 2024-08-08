using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.ViewModels.UserModels;

namespace RealStateApp.Controllers
{
    public class DevController : Controller
    {

        private readonly IServiceManager _serviceManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DevController(IServiceManager serviceManager, IHttpContextAccessor httpContextAccessor)
        {
            _serviceManager = serviceManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            List<UserViewModel> vmList = (await _serviceManager.user.GetAllDevelopersAsync()).ToList() ?? new();
            return View(vmList);
        }

        public async Task<IActionResult> Save()
        {
            return View(new SaveUserViewModel());
        }

        public async Task<IActionResult> Save(SaveUserViewModel vm)
        {
            return View(new SaveUserViewModel());
        }
        

    }
}
