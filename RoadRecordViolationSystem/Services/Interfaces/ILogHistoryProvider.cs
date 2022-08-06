using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Services.Interfaces
{
   public interface ILogHistoryProvider<T> where T: class
    {
        Task<List<T>> GetLogHistoriesAsync();
        Task DeleteAsync(int logId);

        Task DeleteAllAsync(string userId);
    }
}
