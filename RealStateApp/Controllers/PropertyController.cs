using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.Helpers;
using RealStateApp.Models;
using System.Diagnostics;
using RealStateApp.Core.Application.Enums;
using RealStateApp.Core.Application.ViewModels.PropertyModels;
using RealStateApp.Core.Application.DataTransferObjects.Account;

namespace RealStateApp.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IServiceManager _serviceManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;

        public PropertyController(IServiceManager serviceManager, IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _serviceManager = serviceManager;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<IActionResult> Index()
        {
            var trackChanges = true;
            var properties = await _serviceManager.Property.GetAllViewModel(new List<string> { "Amenities" }, trackChanges);

            return View(properties);
        }

        public async Task<IActionResult> Favorites()
        {
            if (_httpContextAccessor.HttpContext != null)
            {
                var user = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>(Roles.Client.ToString());

                if (user == null)
                {
                    return RedirectToRoute(new { Controller = "Login", Action = "Login" });
                }

                List<PropertyViewModel> favoriteProperties = await _serviceManager.Favorite.GetFavoritePropertiesByUserId(user.Id);
                return View(favoriteProperties);
            }

            return View();
        }

        public async Task<IActionResult> Save()
        {
            await Task.Run(() => { });
            return View(new SavePropertyViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Save(PropertyViewModel vm)
        {
            await Task.Run(() => { });
            return View();
        }

        // TODO: Make the views send you to the single page using asp-route-id on each property card
        public async Task<IActionResult> Single(string id)
        {
            var property = await _serviceManager.Property.GetByIdSaveViewModel(id);

            if (property == null)
            {
                return NotFound();
            }

            var agent = await _userService.GetUserByUsernameAsync(property.User_Id);

            if (agent != null)
            {
                ViewBag.AgentName = $"{agent.FirstName} {agent.LastName}";
                ViewBag.AgentTitle = "Real Estate Agent"; // You can replace this with a dynamic value if available
                ViewBag.AgentPhone = agent.Phone;
                ViewBag.AgentWhatsapp = agent.Phone; // Assuming the WhatsApp number is the same as the phone number
                ViewBag.AgentEmail = agent.Email;
                ViewBag.AgentImage = agent.Image; // Assuming this is the image byte array
            }

            return View(property);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

