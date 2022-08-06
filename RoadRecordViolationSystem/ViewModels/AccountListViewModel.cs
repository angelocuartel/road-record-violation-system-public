using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.ViewModels
{
    public class AccountListViewModel
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string AccountId { get; set; }
        public int InfoId { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
        public string ProfilePicture { get; set; }
    }
}
