using Microsoft.EntityFrameworkCore;
using RoadRecordViolationSystem.Data;
using RoadRecordViolationSystem.Models;
using RoadRecordViolationSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Services.Implementations
{
    public class TicketInformationService : ITicketRepository<ViolatorTicketInformation>
    {
        private readonly AppDBContext _context;
        public TicketInformationService(AppDBContext context)
        {
            _context = context;
        }
        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ViolatorTicketInformation>> GetAllAsync()
        {
            return await _context.ViolatorTicketInformations.ToListAsync();
        }

        public async Task<ViolatorTicketInformation> GetByIdAsync(int id)
        {
            return await _context.ViolatorTicketInformations.FindAsync(id);
        }

        public ViolatorTicketInformation GetDetailsByQrHash(string qrHash)
        {
            var model = _context.ViolatorTicketInformations.AsNoTracking()
                .FirstOrDefault(i => i.QrSecurityHash == qrHash);

            return model;
        }

        public ViolatorTicketInformation GetDetailsByTicketNo(string ticketNo)
        {
            return  _context.ViolatorTicketInformations.AsNoTracking()
            .Where(i => i.TicketNo == ticketNo)
            .FirstOrDefault();
        }

        public int GetLatestId()
        {
           return _context.ViolatorTicketInformations.OrderByDescending(i => i.Id)
                .Select(i => i.Id)
                .FirstOrDefault();
        }

        public int GetRecordCount()
        {
            throw new NotImplementedException();
        }

        public async Task InsertAsync(ViolatorTicketInformation obj)
        {
            await _context.ViolatorTicketInformations.AddAsync(obj);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ViolatorTicketInformation obj)
        {
            _context.ViolatorTicketInformations.Update(obj);
            await _context.SaveChangesAsync();
        }
    }
}
