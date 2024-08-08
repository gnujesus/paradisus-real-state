using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.Helpers;
using RealStateApp.Models;
using System.Diagnostics;
using RealStateApp.Core.Application.Enums;
using RealStateApp.Core.Application.ViewModels.PropertyModels;
using RealStateApp.Core.Application.DataTransferObjects.Account;
using RealStateApp.Core.Application.Services.MainServices;
using RealStateApp.Core.Application.ViewModels.PropertyAmenityModels;

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
            AuthenticationResponse user = HttpContext.Session.Get<AuthenticationResponse>("user") ?? new();

            if (user.Roles[0] == "Guest")
            {
                return RedirectToRoute(new { Controller = "Login", Action = "Login" });
            }

            List<PropertyViewModel> favoriteProperties = await _serviceManager.Favorite.GetFavoritePropertiesByUserId(user.Id) ?? new();
            return View(favoriteProperties);
        }

        public async Task<IActionResult> Save()
        {

            AuthenticationResponse user = HttpContext.Session.Get<AuthenticationResponse>("user") ?? new();

            if (user.Roles[0] == "Guest")
            {
                return RedirectToRoute(new { Controller = "Login", Action = "Index" });
            }

            var propertyTypes = await _serviceManager.TypeProperty.GetAllViewModel();
            var saleTypes = await _serviceManager.TypeSale.GetAllViewModel();
            var amenities = await _serviceManager.Amenity.GetAllViewModel();

            ViewBag.PropertyTypes = propertyTypes;
            ViewBag.SaleTypes = saleTypes;
            ViewBag.Amenities = amenities;

            return View(new SavePropertyViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Save(SavePropertyViewModel vm)
        {
            AuthenticationResponse user = HttpContext.Session.Get<AuthenticationResponse>("user") ?? new();

            if (user.Roles[0] == "Guest")
            {
                return RedirectToRoute(new { Controller = "Login", Action = "Index" });
            }

            if (ModelState.IsValid)
            {
                vm.UserId = user.Id;
                await _serviceManager.Property.Add(vm);
                return RedirectToRoute(new { Controller = "Agent", Action = "Home" });
            }

            var propertyTypes = await _serviceManager.TypeProperty.GetAllViewModel();
            var saleTypes = await _serviceManager.TypeSale.GetAllViewModel();
            var amenities = await _serviceManager.Amenity.GetAllViewModel();

            ViewBag.PropertyTypes = propertyTypes;
            ViewBag.SaleTypes = saleTypes;
            ViewBag.Amenities = amenities;

            return View(vm);
        }

        public async Task<IActionResult> Single(string id)
        {
            var property = await _serviceManager.Property.GetByIdSaveViewModel(id);

            if (property == null)
            {
                return NotFound();
            }

            var agent = await _userService.GetUserByIdAsync(property.UserId);

            if (agent != null)
            {
                ViewBag.AgentName = $"{agent.FirstName} {agent.LastName}";
                ViewBag.AgentTitle = "Real Estate Agent"; // You can replace this with a dynamic value if available
                ViewBag.AgentPhone = agent.Phone;
                ViewBag.AgentWhatsapp = agent.Phone; // Assuming the WhatsApp number is the same as the phone number
                ViewBag.AgentEmail = agent.Email;
                ViewBag.AgentImage = agent.Image; // Assuming this is the image byte array
            }

            // Retrieve all amenities related to the property
            var properties = await _serviceManager.Property.GetAllProperties();

            return View(property);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

