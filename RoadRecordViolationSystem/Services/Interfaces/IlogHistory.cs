using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Services.Interfaces
{
   public interface IlogHistory
    {

        Task RecordlogInAsync(string id,string dateIn, string timeIn);
        Task RecordLogOutAsync();

    }
}
