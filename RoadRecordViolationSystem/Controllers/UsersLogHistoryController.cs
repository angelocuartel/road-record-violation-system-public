using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadRecordViolationSystem.Models;
using RoadRecordViolationSystem.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Controllers
{
    [Authorize(Roles = "Administrator")]


    public class UsersLogHistoryController : Controller
    {
        #region private fields
        private readonly ILogHistoryProvider<UsersLogHistory> _historyService;
        #endregion

        #region constructor
        public UsersLogHistoryController(ILogHistoryProvider<UsersLogHistory> historyService)
        {
            _historyService = historyService;
        }
        #endregion

        #region Action methods
        [HttpGet]
        public async  Task<IActionResult> LogHistoryList()
        {
            var result = await _historyService.GetLogHistoriesAsync();

            return View(ExcemptCurrentUserLogHistory(result));
        }

        [HttpGet]
        public async Task<IActionResult> GetLogHistoryListPartial()
        {
            var result = await _historyService.GetLogHistoriesAsync();
            return PartialView(ExcemptCurrentUserLogHistory(result));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteLog(int id)
        {
            await _historyService.DeleteAsync(id);
            return RedirectToAction(nameof(GetLogHistoryListPartial));
        }
        #endregion

        #region private methods
        private List<UsersLogHistory> ExcemptCurrentUserLogHistory(List<UsersLogHistory> histories)
        {
            return histories.Where(i => i.User.UserName != User.Identity.Name)
                .OrderByDescending(i => i.LogId)
                .ToList();
        }
        #endregion

    }
}
