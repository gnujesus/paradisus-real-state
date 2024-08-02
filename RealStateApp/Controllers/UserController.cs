using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.ViewModels.UserModels;
using RealStateApp.Models;
using System.Diagnostics;

namespace RealStateApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IServiceManager _serviceManager;
        private readonly IAccountService _accountService;

        public UserController(IServiceManager serviceManager, IAccountService accountService)
        {
            _serviceManager = serviceManager;
            _accountService = accountService;
        }

        public async Task<IActionResult> Login()
        {
            return View(new LoginViewModel() { });
        }

        public IActionResult Register()
        {
            return View(new SaveUserViewModel() { });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
