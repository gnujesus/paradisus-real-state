﻿using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.ViewModels.TypeSaleModels;

namespace RealStateApp.Controllers
{
    public class SaleTypeController : Controller
    {
        private readonly IServiceManager _serviceManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SaleTypeController(IServiceManager serviceManager, IHttpContextAccessor httpContextAccessor)
        {
            _serviceManager = serviceManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index() {
            List<TypeSaleViewModel> vmList = new();
            var trackChanges = true;
            vmList = await _serviceManager.TypeSale.GetAllViewModel(new List<string> { "Properties" }, trackChanges);
            return View(vmList);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await _serviceManager.TypeSale.Delete(id);
            return RedirectToRoute(new {Controller="SaleType", Action="Index"});
        }

        public IActionResult Save()
        {
            return View(new SaveTypeSaleViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Save(SaveTypeSaleViewModel vm)
        {
            if (ModelState.IsValid)
            {
                await _serviceManager.TypeSale.Add(vm);
                return RedirectToRoute(new {Controller="SaleType", Action="Index"});
            }

            return View(vm);
        }

        public async Task<IActionResult> Update(string id)
        {
            SaveTypeSaleViewModel vm = await _serviceManager.TypeSale.GetByIdSaveViewModel(id);
            return View("Save", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(SaveTypeSaleViewModel vm)
        {
            if (ModelState.IsValid)
            {
                await _serviceManager.TypeSale.Update(vm, vm.Id);
            }

            return RedirectToRoute(new {Controller="SaleType", Action="Index"});
        }

    }
}
