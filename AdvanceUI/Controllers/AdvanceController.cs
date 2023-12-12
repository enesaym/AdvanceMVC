using Microsoft.AspNetCore.Mvc;

namespace AdvanceUI.Controllers
{
    public class AdvanceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
