using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.Services.MainServices;

namespace RealStateApp.Controllers
{
    public class AdminController : Controller
    {

        private readonly IServiceManager _serviceManager;

        public AdminController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public async Task<IActionResult> Home()
        {
            var properties = await _serviceManager.Property.GetAllViewModel();
            var userStatistics = await _serviceManager.user.GetUserStatisticsAsync();

            ViewBag.TotalProperties = properties.Count();

            ViewBag.ActiveAgents = userStatistics.ActiveAgentsCount;
            ViewBag.InactiveAgents = userStatistics.InactiveAgentsCount;
            ViewBag.ActiveClients = userStatistics.ActiveClientsCount;
            ViewBag.InactiveClients = userStatistics.InactiveClientsCount;
            ViewBag.ActiveDevelopers = userStatistics.ActiveDevelopersCount;
            ViewBag.InactiveDevelopers = userStatistics.InactiveDevelopersCount;

            return View();
        }


    }
}
