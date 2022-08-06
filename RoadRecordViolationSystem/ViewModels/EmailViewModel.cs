using RoadRecordViolationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.ViewModels
{
    public class EmailViewModel
    {
        public class ViolationViewModel
        {
            public string ViolatorName { get; set; }
            public IEnumerable<Violations> Violations { get; set; }
            public string TicketNumber { get; set; }
            public string Date { get; set; }

            public ViolationViewModel()
            {
                Date = DateTime.UtcNow.ToLongDateString();
            }
        }

        public class EmailContestApproveViewModel
        {
            public string Name { get; set; }
            public string DateSchedule { get; set; }
            public string  TimeSchedule { get; set; }
            public string Mediator { get; set; }
        }

        public class EmailContestRejectViewModel
        {
            public string Enforcer { get; set; }
            public string TicketNo { get; set; }
            public string ReasonForRejection { get; set; }
        }

        public class AccidentViewModel
        {
            public string DriverName { get; set; }
            public string PlaceOfAccident { get; set; }
            public string Time { get; set; }
            public string Date { get; set; }

            public AccidentViewModel()
            {
                Date = DateTime.UtcNow.ToLongDateString();
            }
        }

    }
}
