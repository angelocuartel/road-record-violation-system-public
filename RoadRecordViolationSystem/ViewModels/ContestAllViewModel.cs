using RoadRecordViolationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.ViewModels
{
    public class ContestAllViewModel
    {
        public IEnumerable<ContestListViewModel> ContestListViewModel { get; set; }
        public IEnumerable<ContestSchedule> ApprovedContests { get; set; }
    }
}
