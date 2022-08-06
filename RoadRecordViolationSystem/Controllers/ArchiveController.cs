using Microsoft.AspNetCore.Mvc;
using RoadRecordViolationSystem.Models;
using RoadRecordViolationSystem.Services;
using RoadRecordViolationSystem.Services.Interfaces;
using RoadRecordViolationSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Controllers
{
    public class ArchiveController : Controller
    {
        private readonly IViolatorRepository<Violator> _violatorService;
        private readonly IViolationTypesRepository<ViolationTypes> _VioTypesService;
        private readonly ICrudRepository<Accident> _accidentService;
        public ArchiveController(IViolatorRepository<Violator> violatorService, IViolationTypesRepository<ViolationTypes> VioTypesService, ICrudRepository<Accident> accidentService)
        {
            _violatorService = violatorService;
            _VioTypesService = VioTypesService;
            _accidentService = accidentService;

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ArchiveViewModel archiveModel = await GetArchiveList();
            return View(archiveModel);
        }

        [HttpPost]
        public async Task<IActionResult> RestoreViolator(Violator violator)
        {
            violator.IsArchive = false;
            await _violatorService.UpdateAsync(violator);

            return RedirectToAction(nameof(GetTablePartial));
        }

        [HttpPost]
        public async Task<IActionResult> RestoreAccident(Accident accident)
        {
            accident.IsArchive = false;
            await _accidentService.UpdateAsync(accident);

            return RedirectToAction(nameof(GetTablePartial));
        }

        [HttpPost]
        public async Task<IActionResult> RestoreViolationTypes(ViolationTypes violationTypes)
        {
            violationTypes.IsArchive = false;
            await _VioTypesService.UpdateAsync(violationTypes);

            return RedirectToAction(nameof(GetTablePartial));
        }

        [HttpGet]
        public async Task<IActionResult> GetTablePartial()
        {
            ArchiveViewModel archiveModel = await GetArchiveList();
            return PartialView("/Views/Archive/_ArchiveTablePartial.cshtml", archiveModel);
        }

        public async Task<ArchiveViewModel> GetArchiveList()
        {
            IEnumerable<Violator> archivedViolators = await _violatorService.GetAllAsync();
            IEnumerable<Accident> archivedAccidents = await _accidentService.GetAllAsync();
            IEnumerable<ViolationTypes> archivedVioTypes = await _VioTypesService.GetAllAsync();

            ArchiveViewModel archiveModel = new ArchiveViewModel()
            {
                Accidents = archivedAccidents.Where(i => i.IsArchive),
                ViolationTypes = archivedVioTypes.Where(i => i.IsArchive),
                Violators = archivedViolators.Where(i => i.IsArchive)
            };

            return archiveModel;
        }
    }
}
