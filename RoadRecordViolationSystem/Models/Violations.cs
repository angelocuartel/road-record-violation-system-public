using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Models
{
    public class Violations
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Code { get; set; }
        public decimal Cost { get; set; }
        public int TicketId { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;
        public virtual ViolatorTicketInformation ViolatorTicketInformation { get; set; }
        public string Penalty { get; set; }
        public Violations()
        {
            this.DateAdded = DateTime.UtcNow;
        }
    }
}
