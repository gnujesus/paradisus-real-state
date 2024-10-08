﻿using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.ViewModels.UserModels;

namespace RealStateApp.Components
{
    public class AgentCardViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(UserViewModel vm)
        {
            await Task.Run(() => { });
            return View(vm);
        }
    }
}
