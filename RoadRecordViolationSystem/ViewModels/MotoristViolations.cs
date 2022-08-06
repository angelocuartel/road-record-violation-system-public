using RoadRecordViolationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.ViewModels
{
    public class MotoristViolations
    {
        public List<Violations> Violations { get; set; }
        public  LicenseDetails LicenseDetails { get; set; }
        public Violator Violator { get; set; }
        public VehicleInformation vehicleInformation { get; set; }
        public ViolatorTicketInformation TicketInformation { get; set; }
    }
}
