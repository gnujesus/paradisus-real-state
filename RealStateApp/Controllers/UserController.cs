using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.DataTransferObjects.Account;
using RealStateApp.Core.Application.Enums;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.ViewModels.UserModels;
using RealStateApp.Core.Application.Helpers;
using RealStateApp.Models;
using System.Diagnostics;

namespace RealStateApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IServiceManager _serviceManager;
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(IServiceManager serviceManager, IAccountService accountService, IHttpContextAccessor httpContextAccessor)
        {
            _serviceManager = serviceManager;
            _accountService = accountService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Login()
        {
            var admin = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>(Roles.Admin.ToString());
            var agent = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>(Roles.Agent.ToString());
            var client = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>(Roles.Client.ToString());

            if (admin != null)
            {
                return RedirectToRoute(new { Controller = "Home", Action = "Index" });
            }
            else if (agent != null)
            {
                return RedirectToRoute(new { Controller = "Home", Action = "Index" });
            }
            else if (client != null)
            {
                // TODO: Make client home page (it must be a different page since it has it's own model and post request)
                return RedirectToRoute(new { Controller = "Home", Action = "Index" });
            }
            else
            {
                return View(new LoginViewModel() { });
            }     
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

