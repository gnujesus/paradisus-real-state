using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Models;
using System.Diagnostics;

namespace RealStateApp.Controllers
{
    public class AgentController : Controller
    {
        private readonly IServiceManager _serviceManager;
        private readonly IAccountService _accountService;

        public AgentController(IServiceManager serviceManager, IAccountService accountService)
        {
            _serviceManager = serviceManager;
            _accountService = accountService;
        }
        public async Task<IActionResult> Index()
        {
            var agents = await _accountService.GetAgentsAsync();
            return View(agents);
        }

        // Agent's home page
        public async Task<IActionResult> Home()
        {
            var vmList = await _serviceManager.Property.GetAllViewModel();
            return View(vmList);
        }

    }
}

