using Microsoft.AspNetCore.Mvc;

namespace RealStateApp.Components
{
    public class BadgeViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
