using RoadRecordViolationSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Models
{
    public class UsersInformation
    {
        public int UserInfoId { get; set; }
        public string GivenName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Gender { get; set; }
        public virtual  ApplicationUser ApplicationUser { get; set; }
       
    }
}
