using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.ViewModels.UserModels;

namespace RealStateApp.Components
{
    public class AgentCardComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(UserViewModel vm)
        {
            return View(vm);
        }
    }
}
