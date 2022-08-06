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
    public class ContestService :  ITicketRepository<Contest>
    {
        private readonly AppDBContext _context;
        public ContestService(AppDBContext context)
        {
            _context = context;
        }
        public async Task DeleteAsync(int id)
        {
            Contest contest = await _context.Contests.FindAsync(id);
            _context.Contests.Remove(contest);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Contest>> GetAllAsync()
        {
            return await _context.Contests
                .AsNoTracking()
                .OrderByDescending(i => i.Id)
                .ToListAsync();
        }

        public async Task<Contest> GetByIdAsync(int id)
        {
            return await _context.Contests.FindAsync(id);
        }

        public Contest GetDetailsByQrHash(string qrHash)
        {
            throw new NotImplementedException();
        }

        public Contest GetDetailsByTicketNo(string ticketNo)
        {
            return  _context.Contests.FirstOrDefault(i => i.TicketNo == ticketNo);
        }

        public int GetLatestId()
        {
            return _context.Contests.OrderByDescending(i => i.Id)
                .Select(i => i.Id)
                .FirstOrDefault();
        }

        public int GetRecordCount()
        {
            return _context.Contests.Count();
        }

        public async Task InsertAsync(Contest obj)
        {
            await _context.Contests.AddAsync(obj);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Contest obj)
        {
            _context.Contests.Update(obj);
            await _context.SaveChangesAsync();
        }
    }
}
