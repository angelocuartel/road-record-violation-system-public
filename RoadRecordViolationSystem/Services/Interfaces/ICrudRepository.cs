using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Services
{
    public interface ICrudRepository <T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task InsertAsync(T obj);
        Task UpdateAsync(T obj);
        Task DeleteAsync(int id);

        int GetRecordCount();

        int GetLatestId();
    }
}
