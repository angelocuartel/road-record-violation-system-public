using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoadRecordViolationSystem.Helpers;
using RoadRecordViolationSystem.Models;
using RoadRecordViolationSystem.Services;
using RoadRecordViolationSystem.Services.Interfaces;
using RoadRecordViolationSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class MotoristController : ControllerBase
    {
        private readonly IFileManager _fileManager;
        private readonly ITicketRepository<ViolatorTicketInformation> _ticketService;
        private readonly ITicketRepository<Contest> _contestService;
        private readonly IViolationRepository<Violations> _violationService;

        public MotoristController(IFileManager fileManager, ITicketRepository<ViolatorTicketInformation> ticketService,
             ITicketRepository<Contest> contestService, IViolationRepository<Violations> violationService)
        {
            _fileManager = fileManager;
            _ticketService = ticketService;
            _contestService = contestService;
            _violationService = violationService;
        }



        [HttpPost]
        [Route("save-contest-image")]
        public async Task<IActionResult> SaveContestImage([FromForm] ContestViewModel model)
        {
            ViolatorTicketInformation ticket = _ticketService.GetDetailsByTicketNo(model.Ticketno);
            Contest contest = _contestService.GetDetailsByTicketNo(model.Ticketno);
            string[] paths = new string[3];

            if (contest != null)
                return BadRequest("Ticket Number already used for contest");

            if (ticket is null)
                return BadRequest("Ticket Number is not valid");

            if (ticket.IsPaid)
                return BadRequest("Ticket Number already paid");

            if (model.LetterContestFile != null)
            {
                string letterPath = _fileManager.GeneratePath(model.LetterContestFile, IFileManager.ExtensionType.Pdf);
                await _fileManager.UploadIFormFileAsync(model.LetterContestFile, letterPath);
                paths[0] = letterPath.ToImageContentPath();
            }
            if (model.ProofOfContestImageVid != null)
            {
                if (model.ProofOfContestImageVid.Length > 104857600)
                {
                    return BadRequest("File/Video is too large. please upload less than or equal to 100mb");
                }
                else
                {
                    string videoPath = _fileManager.GeneratePath(model.ProofOfContestImageVid, IFileManager.ExtensionType.Video);
                    await _fileManager.UploadIFormFileAsync(model.ProofOfContestImageVid, videoPath);
                    paths[1] = videoPath.ToImageContentPath();
                }
            }
            if (model.OrCrImage != null)
            {
                string orcrPath = _fileManager.GeneratePath(model.OrCrImage, IFileManager.ExtensionType.Image);
                await _fileManager.UploadIFormFileAsync(model.OrCrImage, orcrPath);
                paths[2] = orcrPath.ToImageContentPath();
            }

            var test = HttpContext.Session.GetString("OrCrPath");
            return Ok(new { letterPath = paths[0], vidPath = paths[1], ocPath = paths[2] } );
        }

        [HttpPost]
        [Route("save-contest")]
        public async Task<IActionResult> SaveContest([FromForm] Contest contest)
        {
            ViolatorTicketInformation ticket = _ticketService.GetDetailsByTicketNo(contest.TicketNo);
            var test = HttpContext.Session.GetString("OrCrPath");
            contest.ViolatorId = ticket.ViolatorId;
            contest.AgainstEnforcer = ticket.PrintedBy;
            contest.IsApproved = false;

            await _contestService.InsertAsync(contest);

            
            return Ok();
        }


        [HttpGet]
       // [ValidateAntiForgeryToken]
        [Route("get-violations/{ticketNo}")]
        public async Task<IActionResult> GetViolationsByTicketNo(string ticketNo)
        {
            ViolatorTicketInformation ticket = _ticketService.GetDetailsByTicketNo(ticketNo);

            if (ticket is null)
                return BadRequest("Ticket Number is not valid");

            if (ticket.IsPaid)
                return BadRequest("Ticket Number already paid");

            var violationlist = await _violationService.GetAllByTicketNo(ticketNo);

            if (violationlist is null || violationlist.Count < 1)
                return BadRequest("No records found");


            return Ok(violationlist);
        }
    }
}
