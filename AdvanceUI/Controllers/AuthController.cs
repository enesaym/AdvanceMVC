﻿using AdvanceUI.ConnectAPI;

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
                    new Claim(ClaimTypes.NameIdentifier,userInfo.ID),
					new Claim(ClaimTypes.Role,userInfo.TitleName),
					new Claim(ClaimTypes.UserData,userInfo.TitleID),
				};

				var userIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
				var userpri = new ClaimsPrincipal(userIdentity);

                //20 dk sonra logouta atar
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userpri, new AuthenticationProperties() { ExpiresUtc = DateTimeOffset.Now.AddMinutes(20) }); // UI da authorize yapıyoruz kişiyi
                
             
                return RedirectToAction("Index", "Home");
				
			}
			else
			{
                ViewBag.ErrorMessage = "Geçersiz e-posta veya şifre.";
                ModelState.AddModelError(string.Empty, "Geçersiz e-posta veya şifre.");
                return View(dto);
				
			}
		
		}
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync();
			return RedirectToAction("Login", "Auth");
		}


		[HttpPost]
		public async Task<IActionResult> Register(EmployeeRegisterDTO dto)
		{
			if (!ModelState.IsValid)
			{
				await PopulateDropdownsInViewBag();
				return View();
			}

			var isRegistered = await _tokenService.Register(dto);
			if (isRegistered)
			{
				TempData["KullaniciDurumu"] = "Kullanıcı başarıyla kaydedilmiştir";
				return RedirectToAction("Login");
			}

			await PopulateDropdownsInViewBag();
			ViewBag.ErrorMessage = "Kullanıcı veritabanında zaten var";
			return View(dto);
		}

		private async Task PopulateDropdownsInViewBag()
		{
			ViewBag.BusinessUnits = await _tokenService.GetAllUnits();
			ViewBag.Titles = await _tokenService.GetAllTitles();
			ViewBag.Employees = await _tokenService.GetEmployeeBase();
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
