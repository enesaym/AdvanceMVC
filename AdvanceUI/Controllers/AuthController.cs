using Microsoft.AspNetCore.Mvc;

namespace AdvanceUI.Controllers
{
    public class AuthController : Controller
    {
        public AuthController()
        {
				
        }
        public IActionResult Login()
		{
			return View();
		}
		public IActionResult Register()
		{
			return View();
		}
	}
}
