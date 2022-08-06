using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Services.Interfaces
{
   public interface IViolationTypesRepository <T> : ICrudRepository<T> where T : class
    {
        bool Exist(string violationName);
    }
}
