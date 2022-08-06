using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Services.Interfaces
{
  public  interface IOfficerInvolveRepository<T> : IInvolveRepository<T>,ICrudRepository<T>,IAddRangeSupport<T> where T :class
    {
        
    }
}
