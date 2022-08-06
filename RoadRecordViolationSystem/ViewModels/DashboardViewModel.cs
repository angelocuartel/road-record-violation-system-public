﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.ViewModels
{
    public class DashboardViewModel
    {
        public int ViolatorCount { get; set; }
        public int PendingContestCount { get; set; }
        public int ApproveContestCount { get; set; }
        public int RejectedContestCount { get; set; }
        public int AccidentCount { get; set; }
        public string TotalPaidAmount { get; set; }

    }

    public class DashboardByMonthViewModel
    {
        public int Jan { get; set; }
        public int Feb { get; set; }
        public int Mar { get; set; }
        public int Apr { get; set; }
        public int May { get; set; }
        public int Jun { get; set; }
        public int Jul { get; set; }
        public int Aug { get; set; }
        public int Sep { get; set; }
        public int Oct { get; set; }
        public int Nov { get; set; }
        public int Dec { get; set; }
    }
}