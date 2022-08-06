using RoadRecordViolationSystem.Utils.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Models
{
    public class ViolatorTicketInformation
    {
        public int Id { get; set; }
        public int ViolatorId { get; set; }
        public int ViolationCount { get; set; }
        public decimal TotalAmountToBePayed { get; set; }
        public string  PrintedBy { get; set; }
        public DateTime DatePrinted { get; set; }
        public string QrSecurityHash { get; set; }

        public string TicketNo { get; set; }
        public bool IsPaid { get; set; }
        public int? vehicleId { get; set; }

        public string ViolatorSignatureImagePath { get; set; }
        public VehicleInformation VehicleInformation { get; set; }
        public Violator Violator { get; set; }
        public List<Violations> Violations { get; set; }
        public DateTime CommencingDateForAdditional { get; set; }
        public ViolatorTicketInformation()
        {
            DatePrinted = DateTime.UtcNow;
            CommencingDateForAdditional = DatePrinted.AddDays(TransactionUtil.CalculateExpirationDay(DatePrinted));
        }
    }
}
