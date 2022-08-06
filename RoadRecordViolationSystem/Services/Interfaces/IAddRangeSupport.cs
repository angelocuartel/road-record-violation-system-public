using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Services.Interfaces
{
  public  interface IAddRangeSupport<T>
    {
        Task InsertRangeAsync(List<T> objs);
    }
}
