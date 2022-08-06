using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadRecordViolationSystem.Models;
using RoadRecordViolationSystem.Services;
using RoadRecordViolationSystem.Services.Interfaces;
using RoadRecordViolationSystem.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Controllers
{
    [Authorize(Roles ="Administrator")]
    public class ViolatorController : Controller
    {
        private readonly IViolatorRepository<Violator> _violator;
        public ViolatorController(IViolatorRepository<Violator> violator)
        {
            _violator = violator;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var violatorList = await _violator.GetAllAsync();
            return View(violatorList);
        }
        [HttpGet]
        public async Task<IActionResult> GetTablePartial()
        {
            var violatorList = await _violator.GetAllAsync();
            return PartialView("/Views/Violator/_ViolationPartial.cshtml",violatorList);
        }
        [HttpGet]
        public IActionResult GetModalPartial([FromServices] ICrudRepository<VehicleInformation> vehicleService, [FromServices] ILicenseDetailsRepository<LicenseDetails> license,Violator violator)
        {
            var licenseDetails = license.GetLicenseByViolatorId(violator.Id);
            var vehicles = vehicleService.GetAllAsync().Result.Where(i => i.ViolatorId == violator.Id).ToList();

            var viewModel = new ViolatorViewModel()
            {
                License = licenseDetails,
                Vehicles = vehicles,
                Violator = violator
            };
            
            return PartialView("Views/Violator/_ViewMoreModalPartial.cshtml", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Archive(Violator violator)
        {
            violator.IsArchive = true;
            await _violator.UpdateAsync(violator);

            return RedirectToAction(nameof(GetTablePartial));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _violator.DeleteAsync(id);
                return RedirectToAction(nameof(GetTablePartial));
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }


        }


    }
}
