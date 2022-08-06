using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Models
{
    public class VehicleInformation
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public string PlateNo { get; set; }
        public string StickerNo { get; set; }
        public string Latitude { get; set; }
        public string longtitude { get; set; }
        public string PlaceOfViolation { get; set; }
        public  int ViolatorId { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;
        public virtual Violator Violator { get; set; }
        public virtual List<ViolatorTicketInformation> violationTickets { get; set; }

    }
}
