using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.Helpers;
using RealStateApp.Core.Application.Enums;
using RealStateApp.Core.Application.ViewModels.PropertyModels;
using RealStateApp.Core.Application.DataTransferObjects.Account;
using RealStateApp.Core.Application.ViewModels.FavoritesModels;

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
                var properties = await _serviceManager.Property.Add(vm);

                List<byte[]> imagesData = new List<byte[]>();

                if (vm.ImagesR != null && vm.ImagesR.Count > 0)
                {
                    foreach (var image in vm.ImagesR)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await image.CopyToAsync(memoryStream);
                            byte[] imgData = memoryStream.ToArray();
                            imagesData.Add(imgData);
                        }
                    }
                }
                _serviceManager.PropertyImage.AddImagesToPropertyAsync(properties.Id, imagesData);

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
            var properties = await _serviceManager.Property.GetAllViewModel();

            // Find the property that matches the given id
            var property = properties.FirstOrDefault(p => p.Id == id);

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
            // var properties = await _serviceManager.Property.GetAllProperties();

            return View(property);
        }

        public async Task<IActionResult> AddFavorite(string propertyId)
        {

            AuthenticationResponse user = HttpContext.Session.Get<AuthenticationResponse>("user") ?? new();

            SaveFavoritesViewModel vm = new()
            {
                UserId = user.Id,
                Property_Id = propertyId,
            };

            await _serviceManager.Favorite.Add(vm);

            var properties = await _serviceManager.Property.GetAllViewModel(new List<string> { "Amenities" }, true);

            return RedirectToRoute(new {Controller="Home", Action="Index"});
        }

    }
}

