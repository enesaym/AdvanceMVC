using AdvanceUI.ConnectAPI;
using AdvanceUI.Models.DTO.Advance;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using System.Threading.Tasks;
using System.Security.Claims;
using AdvanceUI.Models.DTO.Project;
using System.Collections.Generic;
using AdvanceUI.Models.DTO.AdvanceHistory;
using Microsoft.CodeAnalysis;

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

            advanceInsertDTO.EmployeeID = Convert.ToInt32(User.Claims.Where(a => a.Type == ClaimTypes.NameIdentifier).Select(a => a.Value).SingleOrDefault());

            //gelen donen
            var addedAdvance = await _genericService.PostDatas<AdvanceInsertDTO, AdvanceInsertDTO>("Advance/AddAdvance", advanceInsertDTO);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> GetMyAdvances()
        {
            int Employeeid = Convert.ToInt32(User.Claims.Where(a => a.Type == ClaimTypes.NameIdentifier).Select(a => a.Value).SingleOrDefault());

            var advances = await _genericService.GetDatas<List<AdvanceSelectDTO>>($"Advance/GetAdvanceWithAll/{Employeeid}");

            return View(advances);
        }
        [HttpGet]
        public async Task<IActionResult> GetMyApprovalAdvanceDetails(int id)
        {
            int Employeeid = Convert.ToInt32(User.Claims.Where(a => a.Type == ClaimTypes.NameIdentifier).Select(a => a.Value).SingleOrDefault());

            //kartlar icin
            var advancesHistories = await _genericService.GetDatas<List<AdvanceHistorySelectDTO>>($"Advance/GetPendingApprovalAdvance/{Employeeid}");
            //arayuzden tıklanan advance id
            var advanceHistory=advancesHistories.Where(x => x.ID == id).FirstOrDefault();
            //tablo icin
            var advanceHistoryDetails = await _genericService.GetDatas<List<AdvanceHistorySelectDTO>>($"Advance/GetAdvanceHistoryDetails/{advanceHistory.Advance.ID}");
            ViewBag.Details= advanceHistoryDetails;
            return View(advanceHistory);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetMyApprovalAdvances()
        {
            int Employeeid = Convert.ToInt32(User.Claims.Where(a => a.Type == ClaimTypes.NameIdentifier).Select(a => a.Value).SingleOrDefault());

            var advancesHistories = await _genericService.GetDatas<List<AdvanceHistorySelectDTO>>($"Advance/GetPendingApprovalAdvance/{Employeeid}");
      
            return View(advancesHistories);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveAdvance(int AdvanceId,int StatusID)
        {
            AdvanceApproveDTO approve = new AdvanceApproveDTO();
            approve.EmployeeID= Convert.ToInt32(User.Claims.Where(a => a.Type == ClaimTypes.NameIdentifier).Select(a => a.Value).SingleOrDefault());
            approve.TitleID = Convert.ToInt32(User.Claims.Where(a => a.Type == ClaimTypes.UserData).Select(a => a.Value).SingleOrDefault());
            approve.AdvanceID = AdvanceId;
            //inputtan alınan deger
            var ApprovedAmount=Request.Form["amount"];
            approve.ApprovedAmount=Convert.ToDecimal(ApprovedAmount);
            var result = await _genericService.PostDatas<AdvanceApproveDTO, AdvanceApproveDTO>("Advance/ApproveAdvance", approve);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> RejectAdvance(int AdvanceId)
        {
            AdvanceRejectDTO reject=new AdvanceRejectDTO();
			reject.EmployeeID = Convert.ToInt32(User.Claims.Where(a => a.Type == ClaimTypes.NameIdentifier).Select(a => a.Value).SingleOrDefault());
            reject.AdvanceID = AdvanceId;
			//gelen donen
			var result = await _genericService.PostDatas<AdvanceRejectDTO, AdvanceRejectDTO>("Advance/RejectAdvance", reject);
			
			return RedirectToAction("Index", "Home");
        }




    }
}
