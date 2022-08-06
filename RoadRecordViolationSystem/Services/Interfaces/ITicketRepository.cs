using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Services.Interfaces
{
    public interface ITicketRepository<T> : ICrudRepository<T> where T : class
    {
        T GetDetailsByTicketNo(string ticketNo);
        T GetDetailsByQrHash(string qrHash);
    }
}
