using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.DataTransferObjects.Account;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.Services.MainServices;
using RealStateApp.Core.Application.ViewModels.UserModels;
using RealStateApp.Middlewares;
using System.Runtime.Intrinsics.X86;

namespace RealStateApp.Controllers
{
    public class DevController : Controller
    {

        private readonly IServiceManager _serviceManager;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DevController(IServiceManager serviceManager, IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            _serviceManager = serviceManager;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
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


        [HttpPost]
        public async Task<IActionResult> Save(SaveUserViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            if (vm.ImageFile != null && vm.ImageFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await vm.ImageFile.CopyToAsync(memoryStream);
                    vm.Image = memoryStream.ToArray();
                    vm.EmailVerified = "True";
                    vm.IsActive = true;
                }
            }

            var origin = Request.Headers["origin"];
            RegisterResponse response = await _userService.RegisterAsync(vm, origin);


            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);
            }

            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }


        [HttpPost]
        public async Task<IActionResult> ToggleStatus(string id)
        {
            SaveUserViewModel vm = await _serviceManager.user.GetUserByIdAsync(id);
            vm.IsActive = !vm.IsActive;
            await _userService.UpdateUserAsync(vm);

            return RedirectToRoute(new { Controller = "Dev", Action = "Index" });
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

            return RedirectToRoute(new { Controller = "Dev", Action = "Index" });
        }

    }
}
