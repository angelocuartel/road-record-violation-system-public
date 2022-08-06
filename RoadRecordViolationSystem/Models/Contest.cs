using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Models
{
    public class Contest
    {
        public int Id { get; set; }
        public int ViolatorId { get; set; }
        public int EnforcerId { get; set; }
        public string TicketNo { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string OrCrImagePath { get; set; }
        public string ContestLetterImagePath { get; set; }
        public string ProofContestVideoImagePath { get; set; }
        public string AgainstEnforcer { get; set; }
        public bool IsApproved { get; set; }
        public bool IsRejected { get; set; }
        public string ReasonOfRejection { get; set; }
        public  ContestSchedule ContestSchedule { get; set; }

        public DateTime DateAdded { get; set; }

        public Contest()
        {
            this.DateAdded = DateTime.Now;
        }
    }
}
