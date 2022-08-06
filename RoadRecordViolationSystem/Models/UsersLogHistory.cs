using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Models
{
    public class UsersLogHistory
    {
        public int LogId { get; set; }
        public string TimeIn { get; set; }
        public string TimeOut { get; set; }
        public string DateIn { get; set; }
        public string DateOut { get; set; }
        public string Id { get; set; }
        public virtual   ApplicationUser  User { get; set; }
    }
}
