using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Utils.Implementation
{
    public interface IEmailTemplate 
    {
        public abstract string GetRazorPath();
    }
}
