using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadRecordViolationSystem.Data;
using RoadRecordViolationSystem.Models;
using RoadRecordViolationSystem.Services;
using RoadRecordViolationSystem.Services.Implementations;
using RoadRecordViolationSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Controllers
{
    [Authorize(Roles="Administrator")]
    public class ViolationTypesController : Controller
    {
        private readonly IViolationTypesRepository<ViolationTypes> _vioTypeService;
        public ViolationTypesController(IViolationTypesRepository<ViolationTypes> vioTypeService)
        {
            _vioTypeService = vioTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var vioList = await _vioTypeService.GetAllAsync();
            return View(vioList);
        }

        [HttpGet]
        public async Task<IActionResult> TableView()
        {
            var violList = await _vioTypeService.GetAllAsync();
            return PartialView(violList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(ViolationTypes vioType)
        {
            if (ModelState.IsValid)
            {
                if(_vioTypeService.Exist(vioType.Name))
                {
                    return Json(new { invalidModel = true, errors = "Violation name already exist!" });
                }

                await _vioTypeService.InsertAsync(vioType);
                return RedirectToAction(nameof(TableView));
            }

            var inputErrors = ModelState.Values
                .SelectMany(i => i.Errors)
                .Select(i => i.ErrorMessage)
                .FirstOrDefault();

            return Json(new { invalidModel = true,errors = inputErrors });
        }

        [HttpGet]
        public async Task<IActionResult> ModalView(int id)
        {
            return PartialView(await _vioTypeService.GetByIdAsync(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ViolationTypes vioType)
        {
            if (ModelState.IsValid)
            {
                await _vioTypeService.UpdateAsync(vioType);
                return RedirectToAction(nameof(TableView));
            }

            return RedirectToAction("/Views/ViolationTypes/ModalView.cshtml", vioType);
        }

        [HttpPost]
        public async Task<IActionResult> Archive(ViolationTypes vioType)
        {
            vioType.IsArchive = true;
            await _vioTypeService.UpdateAsync(vioType);

            return RedirectToAction(nameof(TableView));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _vioTypeService.DeleteAsync(id);
            return RedirectToAction(nameof(TableView));
        }



    }
}
