using RoadRecordViolationSystem.Models;
using System;
using System.Collections.Generic;


namespace RoadRecordViolationSystem.ViewModels
{
    public class TicketViewModel
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string TicketNo { get; set; }
        public bool IsPaid { get; set; }
        public decimal TotalFine { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime CommencingDateForAdditional { get; set; }
        
        public string AddedBy { get; set; }
        public List<Violations> Violations { get; set; }
    }
}
