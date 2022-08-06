using Microsoft.AspNetCore.Http;
using RoadRecordViolationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.ViewModels
{
    public class AccidentRecordViewModel
    {
        public List<AccidentInvolve> Involves { get; set; }
        public Accident Accident { get; set; }

        public List<OfficerInvolve> OfficerInvolves { get; set; }

    }
}
