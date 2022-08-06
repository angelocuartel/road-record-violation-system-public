using Microsoft.AspNetCore.Authorization;
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
    public class DashboardController : Controller
    {
        private readonly IViolatorRepository<Violator> _violatorService;
        private readonly ICrudRepository<Accident> _accidentService;
        private readonly ITicketRepository<ViolatorTicketInformation> _ticketService;
        private readonly ITicketRepository<Contest> _contestService;
        public DashboardController(IViolatorRepository<Violator> violatorService, ICrudRepository<Accident> accidentService, ITicketRepository<ViolatorTicketInformation> ticketService,
            ITicketRepository<Contest> contestService )
        {
            _violatorService = violatorService;
            _accidentService = accidentService;
            _ticketService = ticketService;
            _contestService = contestService;
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult GetDashboardView()
        {
            return View();

        }

        [HttpGet]
        [Authorize(Roles = "Enforcer")]
        public IActionResult ScanQR()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetDashboardDatas()
        {
            try
            {
                IEnumerable<Contest> contest = await _contestService.GetAllAsync();
                var cardDashboard = new DashboardViewModel()
                {
                    ViolatorCount = _violatorService.GetRecordCount(),
                    AccidentCount = _accidentService.GetRecordCount(),
                    TotalPaidAmount = _ticketService.GetAllAsync().Result
                    .Where(i => i.IsPaid)
                    .Sum(i => i.TotalAmountToBePayed)
                    .ToString("C2", new System.Globalization.CultureInfo("en-PH")),
                    ApproveContestCount = contest.Count(i => i.IsApproved),
                    RejectedContestCount = contest.Count(i => i.IsRejected),
                    PendingContestCount = contest.Count(i =>!i.IsRejected && !i.IsApproved)
                };

                return Ok(cardDashboard);
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult GetDatasMonthByType(string type)
        {
            try
            {
                if(type == "Accident")
                {
                    var accidentList =  _accidentService.GetAllAsync().Result.Where(i => i.DateAdded.Year == DateTime.UtcNow.Year);
                    var DataByMonth = new DashboardByMonthViewModel()
                    {
                        Jan = CountByMonth(1, accidentList),
                        Feb = CountByMonth(2, accidentList),
                        Mar = CountByMonth(3, accidentList),
                        Apr = CountByMonth(4, accidentList),
                        May = CountByMonth(5, accidentList),
                        Jun = CountByMonth(6, accidentList),
                        Jul = CountByMonth(7, accidentList),
                        Aug = CountByMonth(8, accidentList),
                        Sep = CountByMonth(9, accidentList),
                        Oct = CountByMonth(10, accidentList),
                        Nov = CountByMonth(11, accidentList),
                        Dec = CountByMonth(12, accidentList)
                    };


                    return Ok(DataByMonth);
                }
                else
                {
                    var violators =  _violatorService.GetAllAsync().Result.Where(i => i.DateAdded.Year == DateTime.UtcNow.Year);
                    var DataByMonth = new DashboardByMonthViewModel()
                    {
                        Jan = CountByMonth(1, violators),
                        Feb = CountByMonth(2, violators),
                        Mar = CountByMonth(3, violators),
                        Apr = CountByMonth(4, violators),
                        May = CountByMonth(5, violators),
                        Jun = CountByMonth(6, violators),
                        Jul = CountByMonth(7, violators),
                        Aug = CountByMonth(8, violators),
                        Sep = CountByMonth(9, violators),
                        Oct = CountByMonth(10, violators),
                        Nov = CountByMonth(11, violators),
                        Dec = CountByMonth(12, violators)
                    };


                    return Ok(DataByMonth);
                }
            }

            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        private int CountByMonth(int month,IEnumerable<Accident> list)
        {
            return list.Where(i => i.DateAdded.Month == month).Count();
        }
        private int CountByMonth(int month, IEnumerable<Violator> list)
        {
            return list.Where(i => i.DateAdded.Month == month).Count();
        }
    }
}
