using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Models
{
    public class ViolationTypes
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Fee { get; set; }
        public string Code { get; set; }
        public string AddedBy { get; set; }
        public string Penalty { get; set; }
        public bool IsArchive { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;

        public ViolationTypes()
        {
            this.DateAdded = DateTime.UtcNow;
        }
    }
}
