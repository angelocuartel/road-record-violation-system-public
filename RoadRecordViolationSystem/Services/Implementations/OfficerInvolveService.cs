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
    public class OfficerInvolveService : IOfficerInvolveRepository<OfficerInvolve>
    {
        private readonly AppDBContext _context;
        public OfficerInvolveService(AppDBContext context)
        {
            _context = context;
        }
        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<OfficerInvolve>> GetAllAsync()
        {
          return await _context.OfficerInvolves.AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<OfficerInvolve>> GetAllByAccidentId(int id)
        {
            return await _context.OfficerInvolves.AsNoTracking()
                .Where(i => i.AccidentId == id)
                .ToListAsync();
        }

        public async Task<OfficerInvolve> GetByIdAsync(int id)
        {
            return await _context.OfficerInvolves.FindAsync(id);
        }

        public int GetLatestId()
        {
            throw new NotImplementedException();
        }

        public int GetRecordCount()
        {
            throw new NotImplementedException();
        }

        public async Task InsertAsync(OfficerInvolve obj)
        {
            await _context.OfficerInvolves.AddAsync(obj);
            await _context.SaveChangesAsync();
        }

        public async Task InsertRangeAsync(List<OfficerInvolve> objs)
        {
            await _context.OfficerInvolves.AddRangeAsync(objs);
            await _context.SaveChangesAsync();
        }

        public Task UpdateAsync(OfficerInvolve obj)
        {
            throw new NotImplementedException();
        }
    }
}
