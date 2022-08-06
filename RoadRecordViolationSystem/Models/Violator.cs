using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Models
{
    public class Violator
    {
        public int Id { get; set; }
        public string GivenName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Gender { get; set; }
        public int  Age { get; set; }
        public  string  Email { get; set; }
        public  string  PhoneNo { get; set; }
        public  string  Address { get; set; }
        public DateTime DOB { get; set; }
        public string AddedBy { get; set; }

        public bool IsArchive { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;

        public virtual List<Violations> Violations { get; set; }
        public virtual List<VehicleInformation> Vehicles { get; set; }
        public virtual List<ViolatorTicketInformation> ViolatorTicketInformations { get; set; }
        public virtual LicenseDetails LicenseDetails { get; set; }
        public Violator()
        {
            this.DateAdded = DateTime.UtcNow;
        }
    }
}
