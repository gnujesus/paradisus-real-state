using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.ViewModels.PropertyModels;

namespace RealStateApp.Components
{
    public class PropertyCardViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(PropertyViewModel vm)
        {
            await Task.Run(() => { });
            return View(vm);
        }
    }
}
