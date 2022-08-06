using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadRecordViolationSystem.Models;
using RoadRecordViolationSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Controllers
{
    [Authorize]
    public class CurrentUserLogHistoryController : Controller
    {
        private readonly ILogHistoryProvider<UsersLogHistory> _historyService;

        public CurrentUserLogHistoryController(ILogHistoryProvider<UsersLogHistory> historyService)
        {
            _historyService = historyService;
        }

        [HttpGet]
        public async Task<IActionResult> ModalView()
        {
            var result = await _historyService.GetLogHistoriesAsync();

            return PartialView(result.Where(u => u.User.UserName == User.Identity.Name)
                .OrderByDescending(i => i.LogId)
                .ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAll(string id)
        {
           await _historyService.DeleteAllAsync(id);
            return RedirectToAction(nameof(ModalView));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int logId)
        {
            await _historyService.DeleteAsync(logId);
            return RedirectToAction(nameof(ModalView));
        }


    }
}
