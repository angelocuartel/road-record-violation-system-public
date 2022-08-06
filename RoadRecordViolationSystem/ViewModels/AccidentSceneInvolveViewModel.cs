using RoadRecordViolationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.ViewModels
{
    public class AccidentSceneInvolveViewModel
    {
        public IEnumerable<Accident> Accidents { get; set; }
        public IEnumerable<AccidentInvolve> Involves { get; set; }
    }
}
