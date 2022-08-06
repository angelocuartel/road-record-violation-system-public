using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Services.Interfaces
{
  public  interface IViolationRepository<T> : IAddRangeSupport<T>, ICrudRepository<T> where T : class
    {
        Task<List<T>> GetAllByTicketId(int id);
        Task<List<T>> GetAllByTicketNo(string ticketNo);
    }
}
