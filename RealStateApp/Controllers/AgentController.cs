using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.DataTransferObjects.Account;
using RealStateApp.Core.Application.Helpers;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.ViewModels.UserModels;

namespace RealStateApp.Controllers
{
    public class AgentController : Controller
    {
        private readonly IServiceManager _serviceManager;
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;

        public AgentController(IServiceManager serviceManager, IAccountService accountService, IUserService userService)
        {
            _serviceManager = serviceManager;
            _accountService = accountService;
            _userService = userService;

        }
        public async Task<IActionResult> Index()
        {
            var agents = await _serviceManager.user.GetAllAgentsAsync();
            return View(agents);
        }

        // Agent's home page
        public async Task<IActionResult> Home()
        {
            AuthenticationResponse user = HttpContext.Session.Get<AuthenticationResponse>("user") ?? new();

            if (user.Roles.Count() == 0)
            {
                return RedirectToRoute(new { Controller = "Login", Action = "Index" });
            }

            var vmList = await _serviceManager.Property.GetAllViewModel();
            ViewBag.TypeProperties = await _serviceManager.TypeProperty.GetAllViewModel();
            ViewBag.TypeSales = await _serviceManager.TypeSale.GetAllViewModel();
            ViewBag.Amenities = await _serviceManager.Amenity.GetAllViewModel();

            return View(vmList);
        }



        [HttpPost]
        public async Task<IActionResult> Home(string searchString)
        {
            AuthenticationResponse user = HttpContext.Session.Get<AuthenticationResponse>("user") ?? new();

            if (user.Roles.Count() == 0)
            {
                return RedirectToRoute(new { Controller = "Login", Action = "Index" });
            }

            // Get all properties
            var vmList = await _serviceManager.Property.GetAllViewModel();

            // Filter properties by ID (assuming searchString is the property ID)
            if (!string.IsNullOrEmpty(searchString))
            {
                vmList = vmList.Where(p => p.Id.Contains(searchString)).ToList();
            }

            // Populate the ViewBag with additional data as needed
            ViewBag.TypeProperties = await _serviceManager.TypeProperty.GetAllViewModel();
            ViewBag.TypeSales = await _serviceManager.TypeSale.GetAllViewModel();
            ViewBag.Amenities = await _serviceManager.Amenity.GetAllViewModel();

            // Return the filtered list to the view
            return View(vmList);
        }


        [HttpPost]
        public async Task<IActionResult> ToggleStatus(string id)
        {
            SaveUserViewModel vm = await _serviceManager.user.GetUserByIdAsync(id);
            vm.IsActive = !vm.IsActive;
            await _userService.UpdateUserAsync(vm);

            return RedirectToRoute(new { Controller = "Agent", Action = "Index" });
        }

        public async Task<IActionResult> Update(string id)
        {
            SaveUserViewModel vm = await _serviceManager.user.GetUserByIdAsync(id);
            return View("Save", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(SaveUserViewModel vm)
        {
            await _userService.UpdateUserAsync(vm);
            return RedirectToRoute(new { Controller = "Agent", Action = "Index" });
        }
    }
}

