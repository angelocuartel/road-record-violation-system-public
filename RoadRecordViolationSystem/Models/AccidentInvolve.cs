using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Models
{
    public class AccidentInvolve
    {
        public int Id { get; set; }
        public string GivenName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public DateTime DOB { get; set; }
        public int  Age { get; set; }
        public string EmergencyContactNo { get; set; }
        public string Email { get; set; }

        public int AccidentId { get; set; }
        public Accident Accident { get; set; }



    }
}
