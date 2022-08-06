using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Models
{
    public class Settings
    {
        public int Id { get; set; }
        public int AdditionalChargePerDay { get; set; }
        public int DaysBeforeAdditionalApplies { get; set; }
        public bool EnableSms { get; set; }
        public bool EnableEMail { get; set; }
    }
}
