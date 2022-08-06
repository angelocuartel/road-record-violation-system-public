using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Models
{
    public class LicenseDetails
    {
        public int Id { get; set; }
        public string GivenName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Weight { get; set; }
        public string Height { get; set; }
        public string Nationality { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string EyeColor { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string LicenseNo { get; set; }
        public string AddedBy { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;
        public int ViolatorId { get; set; }

        public virtual Violator Violator { get; set; }

        public LicenseDetails()
        {
            this.DateAdded = DateTime.UtcNow;
        }
    }
}
