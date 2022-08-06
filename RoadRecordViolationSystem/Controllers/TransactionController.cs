using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadRecordViolationSystem.Models;
using RoadRecordViolationSystem.Services;
using RoadRecordViolationSystem.Services.Interfaces;
using RoadRecordViolationSystem.Utils.Implementation;
using RoadRecordViolationSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Controllers
{
    [Authorize(Roles ="Administrator")]
    public class TransactionController : Controller
    {
        private readonly ITicketRepository<ViolatorTicketInformation> _ticketService;
        private readonly IViolatorRepository<Violator> _violatorService;
        private readonly IViolationRepository<Violations> _violationService;
        private readonly ICrudRepository<Settings> _settings;
        public TransactionController(ITicketRepository<ViolatorTicketInformation> ticketService, IViolatorRepository<Violator> violatorService,
            IViolationRepository<Violations> violationService, ICrudRepository <Settings> settings)
        {
            _ticketService = ticketService;
            _violatorService = violatorService;
            _violationService = violationService;
            _settings = settings;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await GetViewTickets());
        }
        [HttpGet]
        public async Task<IActionResult> GetTablePartialView()
        {
            return PartialView("/Views/Transaction/_TransactTablePartial.cshtml",await GetViewTickets());
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsPaid(TicketViewModel model)
        {
            var ticketInfo = await _ticketService.GetByIdAsync(model.Id);

            ticketInfo.TotalAmountToBePayed = model.TotalFine;
            ticketInfo.IsPaid = true;

            await _ticketService.UpdateAsync(ticketInfo);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> TransactionView(TicketViewModel model)
        {
            model.Violations = await _violationService.GetAllByTicketId(model.Id);
            int daysPassedAfterExpiration = TransactionUtil.CountValidAdditionalCharge(model.CommencingDateForAdditional.Date);
            Settings setting =  await _settings.GetByIdAsync(1);
            ViewBag.AdditionalPercent =  ((decimal)setting.AdditionalChargePerDay / 100);

            if (daysPassedAfterExpiration > 0)
            {
                var totalAdditionalCharge = TransactionUtil.CalculateAdditionalChargeByDays(daysPassedAfterExpiration, model.TotalFine,ViewBag.AdditionalPercent);
                ViewBag.AdditionalCharge = totalAdditionalCharge;
                
            }

            return PartialView("Views/Transaction/_TransactionPagePartial.cshtml",model);
        }

        private string GetViolatorName(int id)
        {
            var name = _violatorService.GetByIdAsync(id).Result;

            return $"{name.LastName}, {name.GivenName} {name.MiddleName}".ToUpper();
        }

        private async Task<List<TicketViewModel>> GetViewTickets()
        {
            var ticketList = await _ticketService.GetAllAsync();           
            List<TicketViewModel> ticketListView = new List<TicketViewModel>();

            ticketList.ToList().ForEach(i =>
            {

                TicketViewModel ticketViewModel = new TicketViewModel()
                {
                    Name = GetViolatorName(i.ViolatorId),
                    TotalFine = i.TotalAmountToBePayed,
                    AddedBy = i.PrintedBy,
                    DateAdded = i.DatePrinted,
                    TicketNo = i.TicketNo,
                    Id = i.Id,
                    IsPaid = i.IsPaid,
                    CommencingDateForAdditional = i.CommencingDateForAdditional
                };

                ticketListView.Add(ticketViewModel);
            });

            return ticketListView;
        }

    }
}
