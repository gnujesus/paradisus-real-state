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

            switch (user.Roles[0])
            {
                case "Client":
                    return View(vm);

                case "Admin":
                    return View("~/Views/Administrator/Home.cshtml");

                case "Agent":
                    return View("~/Views/Agent/Home.cshtml");

                default:
                    return View(vm);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Index(
            string propertyCode = "",
            string propertyType = "",
            int minPrice = 0,
            int maxPrice = 0,
            int numberOfRooms = 0,
            int numberOfBathrooms = 0)
        {
            var properties = await _serviceManager.Property.GetAllViewModel();

            if (!string.IsNullOrEmpty(propertyCode))
            {
                properties = properties.Where(p => p.Id.Contains(propertyCode, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(propertyType))
            {
                properties = properties.Where(p => p.Type_Property.Name.Equals(propertyType, StringComparison.OrdinalIgnoreCase)).ToList();
            }
           //properties = new();
            var trackChanges = true;
            var properties = await _serviceManager.Property.GetAllViewModel(new List<string> { "Images" }, trackChanges);
=======
            AuthenticationResponse user = HttpContext.Session.Get<AuthenticationResponse>("user") ?? new();

            // Preventing any null references with '?? new()' at the end
            CustomerHomeViewModel vm = new()
            {
                Properties = await _serviceManager.Property.GetAllViewModel(),
            };
>>>>>>> 53c5862e6774daab7eb431db7809cc52c330f69b

            switch (user.Roles[0])
            {
                case "Client":
                    return View(vm);

                case "Admin":
                    return View("~/Views/Administrator/Home.cshtml");

                case "Agent":
                    return View("~/Views/Agent/Home.cshtml");

                default:
                    return View(vm);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Index(
            string propertyCode = "",
            string propertyType = "",
            int minPrice = 0,
            int maxPrice = 0,
            int numberOfRooms = 0,
            int numberOfBathrooms = 0)
        {
            var properties = await _serviceManager.Property.GetAllViewModel();

            if (!string.IsNullOrEmpty(propertyCode))
            {
                properties = properties.Where(p => p.Id.Contains(propertyCode, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(propertyType))
            {
                properties = properties.Where(p => p.Type_Property.Name.Equals(propertyType, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (minPrice > 0)
            {
                properties = properties.Where(p => p.Value_Sale >= minPrice).ToList();
            }

            if (maxPrice > 0)
            {
                properties = properties.Where(p => p.Value_Sale <= maxPrice).ToList();
            }

            if (numberOfRooms > 0)
            {
                properties = properties.Where(p => p.Rooms == numberOfRooms).ToList();
            }
            if (minPrice > 0)
            {
                properties = properties.Where(p => p.Value_Sale >= minPrice).ToList();
            }

            if (maxPrice > 0)
            {
                properties = properties.Where(p => p.Value_Sale <= maxPrice).ToList();
            }

            if (numberOfRooms > 0)
            {
                properties = properties.Where(p => p.Rooms == numberOfRooms).ToList();
            }

            if (numberOfBathrooms > 0)
            {
                properties = properties.Where(p => p.BathRooms == numberOfBathrooms).ToList();
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

