using RoadRecordViolationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.ViewModels
{
    public class ContestListViewModel
    {
        public string Complainant { get; set; }
        public string Enforcer { get; set; }
        public string DateOfviolation { get; set; }
        public int ViolatorId { get; set; }
        public int EnforcerId { get; set; }

        public int ContestId { get; set; }
        public string TicketNo { get; set; }

        public string OrCrPath { get; set; }
        public string LetterPath { get; set; }
        public string VideoProofPath { get; set; }
        public string Time { get; set; }
        public int TicketInfoId { get; set; }
        public string PlaceOfViolation { get; set; }
        public bool IsRejected { get; set; }
        public bool IsApproved { get; set; }
        public string ReasonOfRejection { get; set; }

    }
}
