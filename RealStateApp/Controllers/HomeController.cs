using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.DataTransferObjects.Account;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.Helpers;
using RealStateApp.Core.Application.Enums;
using RealStateApp.Core.Application.ViewModels.PropertyModels;
using RealStateApp.Core.Application.ViewModels.HomeModels;

namespace RealStateApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServiceManager _serviceManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(IServiceManager serviceManager, IHttpContextAccessor httpContextAccessor)
        {
            _serviceManager = serviceManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {

            AuthenticationResponse user = HttpContext.Session.Get<AuthenticationResponse>("user") ?? new();

            // Preventing any null references with '?? new()' at the end
            CustomerHomeViewModel vm = new()
            {
                Properties = await _serviceManager.Property.GetAllViewModel(),
            };

            var propertyImages = await _serviceManager.PropertyImage.GetAllViewModel();

            var propertyTypes = await _serviceManager.TypeProperty.GetAllViewModel();
            ViewBag.PropertyTypes = propertyTypes;

            if (user.Roles.Count > 0)
            {
                switch (user.Roles[0])
                {
                    case "Client":
                        return View(vm);

                    case "Admin":
                        return RedirectToRoute(new {Controller="Admin", Action="Home"});

                    case "Agent":
                        return RedirectToRoute(new {Controller="Agent", Action="Home"});

                    default:
                        return View(vm);
                }
            }

            return View(vm);

        }

        [HttpPost]
        public async Task<IActionResult> Index(
            string propertyCode = "",
            string propertyType = "",
            int minPrice = 0,
            int maxPrice = 0,
            int numberOfRooms = 0,
            int numberOfBathrooms = 0,
            bool _ = true)
        {
            var propertyTypes = await _serviceManager.TypeProperty.GetAllViewModel();
            ViewBag.PropertyTypes = propertyTypes;

            var properties = await _serviceManager.Property.GetAllViewModel();

            if (!string.IsNullOrEmpty(propertyCode))
            {
                properties = properties.Where(p => p.Id.Contains(propertyCode, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(propertyType))
            {
                properties = properties.Where(p => p.TypeProperty != null &&
                                                   p.TypeProperty.Id == propertyType)
                                       .ToList();
            }

            if (minPrice > 0)
            {
                properties = properties.Where(p => p.Price >= minPrice).ToList();
            }

            if (maxPrice > 0)
            {
                properties = properties.Where(p => p.Price <= maxPrice).ToList();
            }

            if (numberOfRooms > 0)
            {
                properties = properties.Where(p => p.Rooms == numberOfRooms).ToList();
            }
            if (minPrice > 0)
            {
                properties = properties.Where(p => p.Price >= minPrice).ToList();
            }

            if (maxPrice > 0)
            {
                properties = properties.Where(p => p.Price <= maxPrice).ToList();
            }

            if (numberOfRooms > 0)
            {
                properties = properties.Where(p => p.Rooms == numberOfRooms).ToList();
            }

            if (numberOfBathrooms > 0)
            {
                properties = properties.Where(p => p.Bathrooms == numberOfBathrooms).ToList();
            }

            CustomerHomeViewModel vm = new()
            {
                Properties = properties,
                PropertyCode = propertyCode,
                PropertyType = propertyType,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                NumberOfRooms = numberOfRooms,
                NumberOfBathrooms = numberOfBathrooms
            };

            return View(vm);
        }
    }
}

