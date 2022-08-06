using RoadRecordViolationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.ViewModels
{
    public class ViolatorViewModel
    {
        public LicenseDetails License { get; set; }
        public Violator Violator { get; set; }
        public  List<VehicleInformation> Vehicles { get; set; }
    }
}
