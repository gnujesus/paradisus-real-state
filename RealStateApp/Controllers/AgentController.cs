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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
