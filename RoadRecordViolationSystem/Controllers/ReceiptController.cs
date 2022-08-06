using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RoadRecordViolationSystem.Models;
using RoadRecordViolationSystem.Utils.Implementation;
using RoadRecordViolationSystem.ViewModels;
using System.Security.Claims;

namespace RoadRecordViolationSystem.Controllers
{

    [Authorize(Roles ="Enforcer")]
    public class ReceiptController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;

        public ReceiptController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpPost]
        public IActionResult Index(MotoristViolations model)
        {
            ViewBag.QrLicense = QrUtil.Generate(model.TicketInformation.QrSecurityHash);
             return PartialView("/Views/Receipt/_ReceiptPartial.cshtml", model);
        }

      /*  [HttpGet]
        public IActionResult Index()
        {
            return PartialView("/Views/Receipt/_ReceiptPartial.cshtml");
        }*/


    }
}
