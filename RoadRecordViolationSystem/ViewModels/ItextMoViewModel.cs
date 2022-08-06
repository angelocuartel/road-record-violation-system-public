using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.ViewModels
{
    public class ItextMoViewModel
    {
        public class ViolationMessageViewmodel
        {
            public string FullName { get; set; }
            public string  TicketNumber { get; set; }
        }

        public class AccidentMessageViewModel
        {
            public string FullName { get; set; }
            public string Time { get; set; }
            public string Place { get; set; }
        }

        public class RejectedMessageViewModel
        {
            public string Complainant { get; set; }
            public string ReasonForRejection { get; set; }
        }

        public class ApprovedMessageViewModel
        {
            public string Complainant { get; set; }
            public string TicketNo { get; set; }

            public string ScheduleDate { get; set; }
            public string ScheduleTime { get; set; }
            public string Mediator { get; set; }
        }
    }
}
