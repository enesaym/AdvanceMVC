using Microsoft.AspNetCore.Mvc;

namespace AdvanceUI.ViewComponents
{
    public class SideBarViewComponent :ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }

    }
}
