using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Models
{
    public class Accident
    {
        public int Id { get; set; }

        public string AccidentImage { get; set; }
        public string Description { get; set; }
        public string Place { get; set; }
        public string AddedBy { get; set; }

        public string Latitude { get; set; }
        public string longtitude { get; set; }
        public bool IsArchive { get; set; }
        public DateTime DateAdded { get; set; }
        public List<AccidentInvolve> AccidentInvolves { get; set; }
        public List<OfficerInvolve> OfficerInvolves { get; set; }

        public Accident()
        {
            DateAdded = DateTime.UtcNow;
        }
    }
}
