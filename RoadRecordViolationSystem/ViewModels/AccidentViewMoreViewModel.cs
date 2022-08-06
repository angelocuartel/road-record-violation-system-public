using RoadRecordViolationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.ViewModels
{
    public class AccidentViewMoreViewModel
    {
        public Accident Accident { get; set; }
        public IEnumerable<AccidentInvolve> Involves { get; set; }
        public IEnumerable<OfficerInvolve> Officers { get; set; }
    }
}
