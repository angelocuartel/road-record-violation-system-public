using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Services.Interfaces
{
    public interface IInvolveRepository<T> :ICrudRepository<T> where T :class
    {
        Task<IEnumerable<T>> GetAllByAccidentId(int id);
    }
}
