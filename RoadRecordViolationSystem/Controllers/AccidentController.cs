
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadRecordViolationSystem.Models;
using RoadRecordViolationSystem.Services;
using RoadRecordViolationSystem.Services.Interfaces;
using RoadRecordViolationSystem.ViewModels;

namespace RoadRecordViolationSystem.Controllers
{
    [Authorize(Roles ="Administrator")]
    public class AccidentController : Controller
    {
        private readonly ICrudRepository<Accident> _accidentService;
        private readonly IInvolveRepository<AccidentInvolve> _involveService;
        private readonly IFileManager _imageManager;
        private readonly IOfficerInvolveRepository<OfficerInvolve> _officerService;

        public AccidentController(ICrudRepository<Accident> accidentService, IInvolveRepository<AccidentInvolve> involveService, IFileManager imageManager, IOfficerInvolveRepository<OfficerInvolve> officerService)
        {
            _accidentService = accidentService;
            _involveService = involveService;
            _imageManager = imageManager;
            _officerService = officerService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await GetAccidentList());
        }

        [HttpGet]
        public async Task<IActionResult> GetAccidentPartial()
        {
            return PartialView("/Views/Accident/_AccidentTablePartial.cshtml",await GetAccidentList());
        }

        [HttpGet]
        public async Task<IActionResult> ViewAccidentPartial(int id)
        {
            Accident accident = await _accidentService.GetByIdAsync(id);
            IEnumerable<AccidentInvolve> involves = await _involveService.GetAllByAccidentId(id);
            IEnumerable<OfficerInvolve> officers = await _officerService.GetAllByAccidentId(id);

            AccidentViewMoreViewModel model = new AccidentViewMoreViewModel()
            {
                Accident = accident,
                Officers = officers,
                Involves = involves
            };

            return PartialView("/Views/Accident/_ViewAccidentPartial.cshtml",model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
           Accident accident = await _accidentService.GetByIdAsync(id);

            if (!string.IsNullOrEmpty(accident.AccidentImage))
            _imageManager.Delete(accident.AccidentImage);

            await _accidentService.DeleteAsync(id);
            return RedirectToAction(nameof(GetAccidentPartial));
        }

        [HttpPost]
        public async Task<IActionResult> Archive(Accident accident)
        {
            accident.IsArchive = true;
            await _accidentService.UpdateAsync(accident);

            return RedirectToAction(nameof(GetAccidentPartial));
        }

        private async Task<AccidentSceneInvolveViewModel> GetAccidentList()
        {
            IEnumerable<Accident> accidentList = await _accidentService.GetAllAsync();
            IEnumerable<AccidentInvolve> involvesList = await _involveService.GetAllAsync();

            AccidentSceneInvolveViewModel model = new AccidentSceneInvolveViewModel()
            {
                Accidents = accidentList,
                Involves = involvesList
            };

            return model;
        }
    }
}
