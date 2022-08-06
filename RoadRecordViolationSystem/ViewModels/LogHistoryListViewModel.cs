using RoadRecordViolationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.ViewModels
{
    public class LogHistoryListViewModel
    {
        public UsersLogHistory LogHistory { get; set; }
        public string Profile { get; set; }
        public string UserName { get; set; }
    }
}
