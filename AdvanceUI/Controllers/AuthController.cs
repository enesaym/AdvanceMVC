using AdvanceUI.ConnectAPI;
using AdvanceUI.Helpers;
using AdvanceUI.Models.DTO.Employee;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AdvanceUI.Controllers
{
    public class AuthController : Controller
    {
		TokenService _tokenService;
        public AuthController(TokenService tokenService)
        {
			_tokenService=tokenService;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
		public async Task<IActionResult> Login(EmployeeLoginDTO dto)
		{
			var token = await _tokenService.GetToken(dto);
			if (token != "")
			{
				HttpContext.Response.Cookies.Append("token", token, new CookieOptions { Expires = System.DateTimeOffset.Now.AddMinutes(20),/* Domain = "APISample"*/ });

				//mvc session cookie auth
				// signin yapmak gerekir => ya bunla Identity ya da session yukarda olan
				string id=TokenHelper.GetIdFromToken(token);
				var claims = new List<Claim>()
				{
					new Claim(ClaimTypes.Name,dto.Email)
				};

				var userIdentity = new ClaimsIdentity(claims, "login");
				var userpri = new ClaimsPrincipal(userIdentity);

				await HttpContext.SignInAsync(userpri); // UI da authorize yapıyoruz kişiyi

				// Çıkış için
				//await HttpContext.SignOutAsync();
				return RedirectToAction("Index", "Home");
				//
				//return RedirectToAction("Index2", new { dto = dto }); posta yönlendirmek için parametre verdik routevalues
			}
			else
			{
                ViewBag.ErrorMessage = "Geçersiz e-posta veya şifre.";
                ModelState.AddModelError(string.Empty, "Geçersiz e-posta veya şifre.");
                return View(dto);
				
			}
		
		}


        [HttpPost]
        public async Task<IActionResult> Register(EmployeeRegisterDTO dto)
        {
            var donendeger = await _tokenService.Register(dto);
            if (donendeger)
            {
                TempData["KullaniciDurumu"] = "Kullanıcı basariyla kayit edilmistir";
                return RedirectToAction("Login");
            }

            return View();
        }


		[HttpGet]
        public async Task<IActionResult> Register()
		{
            ViewBag.BusinessUnits = await _tokenService.GetAllUnits();
            ViewBag.Titles = await _tokenService.GetAllTitles();
            ViewBag.Employees = await _tokenService.GetEmployeeBase();
            return View();
		}
	}
}
