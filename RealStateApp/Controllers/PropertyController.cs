using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.Helpers;
using RealStateApp.Models;
using System.Diagnostics;
using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Application.Enums;
using RealStateApp.Core.Application.ViewModels.PropertyModels;

namespace RealStateApp.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IServiceManager _serviceManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PropertyController(IServiceManager serviceManager, IHttpContextAccessor httpContextAccessor)
        {
            _serviceManager = serviceManager;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<IActionResult> Index()
        {
            var properties = await _serviceManager.Property.GetAllViewModel();

            return View(properties);
        }

        public async Task<IActionResult> Favorites()
        {

            List<PropertyViewModel> favoriteProperties = new();
            var user = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>(Roles.Client.ToString());

            if (user == null)
            {
                return RedirectToRoute(new {Controller="Login", Action="Login"});
            }
            favoriteProperties = await _serviceManager.Favorite.GetFavoritePropertiesByUserId(user.Id);
            return View(favoriteProperties);
        }


        public async Task<IActionResult> Save()
        {
            return View(new PropertyViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Save(PropertyViewModel vm)
        {
            return View();
        }

        // TODO: Single page of the selected property 
        // the idea is that, if you click into a property, 
        // it'll send you to a page where the data rendered
        // will depend on the clicked property

        public async Task<IActionResult> Single(string id)
        {
            var favorite = await _serviceManager.Property.GetByIdSaveViewModel(id);
            return View(favorite);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

