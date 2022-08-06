using Microsoft.AspNetCore.Mvc;
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
    public class ViolationService : IViolationRepository<Violations>, IAddRangeSupport<Violations>
    {
        private readonly AppDBContext _context;
        private readonly ITicketRepository<ViolatorTicketInformation> _ticketService;
        public ViolationService(AppDBContext context, [FromServices] ITicketRepository<ViolatorTicketInformation> ticketService)
        {
            _context = context;
            _ticketService = ticketService;
        }
        public async Task DeleteAsync(int id)
        {
            _context.Violations.Remove(await _context.Violations.FindAsync(id));
           await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Violations>> GetAllAsync()
        {
            return await _context.Violations.AsNoTracking()
                .OrderByDescending(i => i.Id)
                .ToListAsync();
        }

        public async Task<List<Violations>> GetAllByTicketId(int id)
        {
            return await _context.Violations.Where(i => i.TicketId == id).ToListAsync();
        }

        public async Task<List<Violations>> GetAllByTicketNo(string ticketNo)
        {
           var ticket =  _ticketService.GetDetailsByTicketNo(ticketNo);

            if(ticket is null)
                return null;
            return await _context.Violations.AsNoTracking()
                .Where(i => i.TicketId == ticket.Id)
                .ToListAsync();
        }

        public async Task<Violations> GetByIdAsync(int id)
        {
            return await _context.Violations.FindAsync(id);
        }

        public int GetLatestId()
        {
            throw new NotImplementedException();
        }

        public int GetRecordCount()
        {
            throw new NotImplementedException();
        }

        public async Task InsertAsync(Violations obj)
        {
            await _context.Violations.AddAsync(obj);
            await _context.SaveChangesAsync();
        }


        public async Task InsertRangeAsync(List<Violations> objs)
        {
            await _context.Violations.AddRangeAsync(objs);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Violations obj)
        {
             _context.Violations.Update(obj);
           await _context.SaveChangesAsync();
        }
    }
}
