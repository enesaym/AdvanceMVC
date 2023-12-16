using AdvanceUI.ConnectAPI;
using AdvanceUI.Models.DTO.Advance;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using System.Threading.Tasks;
using System.Security.Claims;
using AdvanceUI.Models.DTO.Project;
using System.Collections.Generic;

namespace AdvanceUI.Controllers
{
    public class AdvanceController : Controller
    {
        GenericService _genericService;
        public AdvanceController(GenericService genericservice)
        {
                _genericService = genericservice;
        }
        [HttpGet]
        public async Task<IActionResult> AddAdvance()
        {
            int employeeID = Convert.ToInt32(User.Claims.Where(a => a.Type == ClaimTypes.NameIdentifier).Select(a => a.Value).SingleOrDefault());;

            string url = $"Project/{employeeID}";
            var projects = await _genericService.GetDatas<List<ProjectSelectDTO>>(url);
            ViewBag.Projects=projects;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAdvance(AdvanceInsertDTO advanceInsertDTO)
        {
          
            advanceInsertDTO.EmployeeID=Convert.ToInt32(User.Claims.Where(a => a.Type ==ClaimTypes.NameIdentifier).Select(a => a.Value).SingleOrDefault());

            //gelen donen
            var addedAdvance = await _genericService.PostDatas<AdvanceInsertDTO, AdvanceInsertDTO>("Advance/AddAdvance", advanceInsertDTO);
            return RedirectToAction("Index","Home");
        }
    }
}
