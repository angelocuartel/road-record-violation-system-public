using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.ViewModels
{
    public class ChangeAddPasswordViewModel
    {
        public string Password { get; set; }
        public string UserName { get; set; }
        public string AdminPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
