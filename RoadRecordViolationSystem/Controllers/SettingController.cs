using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadRecordViolationSystem.Models;
using RoadRecordViolationSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Controllers
{

    [Authorize(Roles="Administrator")]
    public class SettingController : Controller
    {
        private readonly ICrudRepository<Settings> _setting;
        public SettingController(ICrudRepository<Settings> setting)
        {
            _setting = setting;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _setting.GetByIdAsync(1));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Settings obj)
        {
            await _setting.UpdateAsync(obj);

            return Ok();
        }

    }
}
