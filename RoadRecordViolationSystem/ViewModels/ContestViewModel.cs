using Microsoft.AspNetCore.Http;
using RoadRecordViolationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.ViewModels
{
    public class ContestViewModel
    {
        public string Ticketno { get; set; }
        public IFormFile OrCrImage { get; set; }
        public IFormFile ProofOfContestImageVid { get; set; }
        public IFormFile LetterContestFile { get; set; }
    }
}
