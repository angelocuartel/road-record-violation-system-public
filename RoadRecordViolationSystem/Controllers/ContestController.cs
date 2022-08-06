using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadRecordViolationSystem.Models;
using RoadRecordViolationSystem.Services;
using RoadRecordViolationSystem.Services.Interfaces;
using RoadRecordViolationSystem.Utils.EmailTemplates;
using RoadRecordViolationSystem.Utils.Implementation;
using RoadRecordViolationSystem.Utils.Interface;
using RoadRecordViolationSystem.Utils.MessageTemplates;
using RoadRecordViolationSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static RoadRecordViolationSystem.ViewModels.ItextMoViewModel;

namespace RoadRecordViolationSystem.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ContestController : Controller
    {

        private readonly IFileManager _fileManager;
        private readonly ITicketRepository<ViolatorTicketInformation> _ticketService;
        private readonly ITicketRepository<Contest> _contestService;
        private readonly IViolatorRepository<Violator> _violatorService;
        private readonly ICrudRepository<VehicleInformation> _vehicleService;
        private readonly ICrudRepository<ContestSchedule> _scheduleService;
        private readonly IEmail _email;

        public ContestController(IFileManager fileManager, ITicketRepository<ViolatorTicketInformation> ticketService,
             ITicketRepository<Contest> contestService, IViolatorRepository<Violator> violatorService,ICrudRepository<VehicleInformation> vehicleService,
             ICrudRepository<ContestSchedule> scheduleService, IEmail email)
        {
            _fileManager = fileManager;
            _ticketService = ticketService;
            _contestService = contestService;
            _violatorService = violatorService;
            _vehicleService = vehicleService;
            _scheduleService = scheduleService;
            _email = email;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            return View(await GetAllContest());
        }

        [HttpGet]
        public async Task<IActionResult> GetTablePartial()
        {
            return PartialView("/Views/Contest/_ContestTablePartial.cshtml",await GetAllContest());
        }


        [HttpGet]
        public FileResult DownloadLetterContest(string path,string complainant)
        {
            return File(System.IO.File.ReadAllBytes(path.Replace("~","wwwroot")), System.Net.Mime.MediaTypeNames.Application.Pdf, $"{complainant.ToUpper()}_LETTER_FOR_CONTEST.pdf");
        }

        [HttpGet]
        public IActionResult ViewOrCrImagePartial(string path)
        {
            ViewBag.ImagePath = path;
            return PartialView("/Views/Contest/OrCrImageViewPartial.cshtml");
        }


        [HttpGet]
        public  IActionResult RejectPartial(int contestId,string complainant)
        {
            ViewBag.ContestId = contestId;
            ViewBag.Complainant = complainant;

            return PartialView("/Views/Contest/_RejectModalPartial.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> RejectContestApplication(int contestId, string reason,string complainant)
        {
            Contest contest = await _contestService.GetByIdAsync(contestId);
            contest.ReasonOfRejection = reason;
            contest.IsRejected = true;

            await _contestService.UpdateAsync(contest);

            if (!string.IsNullOrEmpty(contest.Email))
            {
                EmailViewModel.EmailContestRejectViewModel rejectedModel = new EmailViewModel.EmailContestRejectViewModel()
                {
                    Enforcer = contest.AgainstEnforcer,
                    TicketNo = contest.TicketNo,
                    ReasonForRejection = reason
                };

                await _email.SendWithTemplateAsync(contest.Email, "CONTEST APPLICATION REJECTED", rejectedModel, new RejectedEmailTemplate());

                RejectedMessageViewModel textData = new RejectedMessageViewModel()
                {
                    Complainant = complainant,
                    ReasonForRejection = reason
                };
                ItextMoUtil.SendMessage(contest.ContactNo, new RejectedMessageTemplate(textData));
            }

            return RedirectToAction(nameof(GetTablePartial));
        }

        [HttpGet]
        public IActionResult ProofOfVideoPartial(string path)
        {
            ViewBag.VideoPath = path;
            return PartialView("/Views/Contest/_ProofVideoPartial.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRejectedContest(int contestId)
        {
            Contest contest = await _contestService.GetByIdAsync(contestId);

            _fileManager.Delete(contest.OrCrImagePath);
            _fileManager.Delete(contest.ContestLetterImagePath);

            if(!string.IsNullOrEmpty(contest.ProofContestVideoImagePath))
                _fileManager.Delete(contest.ProofContestVideoImagePath);

             await _contestService.DeleteAsync(contestId);
            return RedirectToAction(nameof(GetTablePartial));

        }

        [HttpGet]
        public IActionResult ApproveModal(int contestId)
        {
            ContestSchedule schedule = new ContestSchedule()
            {
                Status = "APPROVED",
                ContestApplicationId = contestId
            };

            return PartialView("/Views/Contest/_ApprovedModalPartial.cshtml", schedule);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveApplication(ContestSchedule contestSched)
        {
            Contest contest = await _contestService.GetByIdAsync(contestSched.ContestApplicationId);
            contest.IsApproved = true;
            Violator complainant = await _violatorService.GetByIdAsync(contest.ViolatorId);

            await _contestService.UpdateAsync(contest);

            await _scheduleService.InsertAsync(contestSched);

            if(!string.IsNullOrEmpty(contest.Email))
            {
                EmailViewModel.EmailContestApproveViewModel emailModel = new EmailViewModel.EmailContestApproveViewModel()
                {
                    Mediator = contestSched.Mediator,
                    DateSchedule = contestSched.HearingScheduleDate.ToLongDateString(),
                    TimeSchedule = contestSched.HearingScheduleTime.ToLongTimeString()
                };

                await _email.SendWithTemplateAsync(contest.Email, "CONTEST APPLICATION APPROVED", emailModel,new ApproveEmailTemplate());

                ApprovedMessageViewModel textData = new ApprovedMessageViewModel()
                {
                    Complainant = $"{complainant.LastName}, {complainant.GivenName}, {complainant.MiddleName},",
                    ScheduleDate = contestSched.HearingScheduleDate.ToLongDateString(),
                    ScheduleTime = contestSched.HearingScheduleTime.ToLongTimeString(),
                    Mediator = contestSched.Mediator,
                    TicketNo = contest.TicketNo
                };

                ItextMoUtil.SendMessage(contest.ContactNo, new ApproveMessageTemplate(textData));
            }

            return RedirectToAction(nameof(GetTablePartial));
        }


        private async Task<ContestAllViewModel> GetAllContest()
        {
            IEnumerable<ContestSchedule> scheduleList = await _scheduleService.GetAllAsync();
            IEnumerable<Contest> contestList =  await _contestService.GetAllAsync();
            List<ContestListViewModel> contestListView = new List<ContestListViewModel>();

            foreach (var contest in contestList)
            {
                ViolatorTicketInformation ticket = _ticketService.GetDetailsByTicketNo(contest.TicketNo);
                Violator violator = await _violatorService.GetByIdAsync(ticket.ViolatorId);
                VehicleInformation vehicle = await _vehicleService.GetByIdAsync((int)ticket.vehicleId);


                ContestListViewModel contestView = new ContestListViewModel()
                {
                    Complainant = $"{violator.LastName}, {violator.GivenName} {violator.MiddleName}",
                    ViolatorId = violator.Id,
                    ContestId = contest.Id,
                    DateOfviolation = ticket.DatePrinted.ToLongDateString(),
                    Time = ticket.DatePrinted.ToLongTimeString(),
                    Enforcer = ticket.PrintedBy,
                    OrCrPath = contest.OrCrImagePath,
                    LetterPath = contest.ContestLetterImagePath,
                    VideoProofPath = contest.ProofContestVideoImagePath,
                    PlaceOfViolation = vehicle.PlaceOfViolation,
                    TicketNo = ticket.TicketNo,
                    IsApproved = contest.IsApproved,
                    IsRejected = contest.IsRejected,
                    ReasonOfRejection = contest.ReasonOfRejection
                };

                contestListView.Add(contestView);
            }

            ContestAllViewModel model = new ContestAllViewModel()
            {
                ContestListViewModel = contestListView,
                ApprovedContests = scheduleList
            };

            return model;      
        }


      
    }
}
