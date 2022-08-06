using Microsoft.AspNetCore.Mvc;
using RoadRecordViolationSystem.Models;
using RoadRecordViolationSystem.Services;
using RoadRecordViolationSystem.Services.Interfaces;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Controllers
{
    public class ViolationController : Controller
    {

        private readonly IViolationRepository<Violations> _violationService;

        public ViolationController(IViolationRepository<Violations> violationService)
        {
            _violationService = violationService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetViolationsByTicketNo(string TicketNo)
        {
            var violationlist = await _violationService.GetAllByTicketNo(TicketNo);

            if (violationlist is null || violationlist.Count < 1)
                return BadRequest( "No records found");


            return PartialView("/Views/Violation/_DriverViolationTablePartial.cshtml",violationlist);
        }

    }
}
