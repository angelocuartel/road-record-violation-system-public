using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int UsersInfoId { get; set; }
        public string AccountStatus { get; set; }
        public virtual UsersInformation UsersInformation { get; set; }
        public string ProfilePicture { get; set; }

        public DateTime? ValidTimeToLogin { get; set; }

        public List<UsersLogHistory> UsersLogHistories { get; set; }
    }
}
