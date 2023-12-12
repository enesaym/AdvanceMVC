using Microsoft.AspNetCore.Mvc;

namespace AdvanceUI.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
