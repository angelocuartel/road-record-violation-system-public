using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Models
{
    public class ContestSchedule
    {
        public int Id { get; set; }
        public int ContestApplicationId { get; set; }
        public string Mediator { get; set; }
        public DateTime HearingScheduleDate { get; set; }
        public DateTime HearingScheduleTime { get; set; }
        public string Status { get; set; }
        public virtual Contest Contest { get; set; }
    }
}
