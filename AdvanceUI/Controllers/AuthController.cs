using AdvanceUI.ConnectAPI;
using AdvanceUI.Helpers;
using AdvanceUI.Models.DTO.Employee;
using AdvanceUI.Models.DTO.UserInfo;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
				//cookie ye token ekler
				HttpContext.Response.Cookies.Append("token", token, new CookieOptions { Expires = System.DateTimeOffset.Now.AddMinutes(20),/* Domain = "APISample"*/ });
				
				UserInfoDTO userInfo=TokenHelper.GetUserInfoFromToken(token);

                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name,userInfo.Name),
                    new Claim(ClaimTypes.NameIdentifier,userInfo.ID)
				};

				var userIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
				var userpri = new ClaimsPrincipal(userIdentity);

                //20 dk sonra logouta atar
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userpri, new AuthenticationProperties() { ExpiresUtc = DateTimeOffset.Now.AddMinutes(20) }); // UI da authorize yapıyoruz kişiyi
                
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
