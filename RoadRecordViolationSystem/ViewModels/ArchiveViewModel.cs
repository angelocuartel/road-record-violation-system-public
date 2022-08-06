using RoadRecordViolationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.ViewModels
{
    public class ArchiveViewModel
    {
        public IEnumerable<Violator> Violators { get; set; }
        public IEnumerable<Accident> Accidents { get; set; }
        public IEnumerable<ViolationTypes> ViolationTypes { get; set; }
    }
}
