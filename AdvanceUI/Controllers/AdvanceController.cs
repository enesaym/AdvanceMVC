using AdvanceUI.ConnectAPI;
using AdvanceUI.Models.DTO.Advance;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using System.Threading.Tasks;
using System.Security.Claims;

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
        public IActionResult AddAdvance()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAdvance(AdvanceInsertDTO advanceInsertDTO)
        {
          
            advanceInsertDTO.EmployeeID=Convert.ToInt32(User.Claims.Where(a => a.Type ==ClaimTypes.NameIdentifier).Select(a => a.Value).SingleOrDefault());

            //gelen donen
            var addedAdvance = await _genericService.PostDatas<AdvanceInsertDTO, AdvanceInsertDTO>("Advance/AddAdvance", advanceInsertDTO);
            return View();
        }
    }
}
