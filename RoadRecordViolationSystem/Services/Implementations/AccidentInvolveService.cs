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
    public class AccidentInvolveService : IInvolveRepository<AccidentInvolve>, IAddRangeSupport<AccidentInvolve>
    {
        private readonly AppDBContext _context;
        public AccidentInvolveService(AppDBContext context)
        {
            _context = context;
        }
        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AccidentInvolve>> GetAllAsync()
        {
          return await _context.AccidentInvolves.AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<AccidentInvolve>> GetAllByAccidentId(int id)
        {
            return await _context.AccidentInvolves.AsNoTracking()
                .Where(i => i.AccidentId == id)
                .ToListAsync();
        }

        public async Task<AccidentInvolve> GetByIdAsync(int id)
        {
            return await _context.AccidentInvolves.FindAsync(id);
        }

        public int GetLatestId()
        {
            throw new NotImplementedException();
        }

        public int GetRecordCount()
        {
            throw new NotImplementedException();
        }

        public async Task InsertAsync(AccidentInvolve obj)
        {
            await _context.AccidentInvolves.AddAsync(obj);
            await _context.SaveChangesAsync();
        }

        public async Task InsertRangeAsync(List<AccidentInvolve> objs)
        {
            await _context.AccidentInvolves.AddRangeAsync(objs);
        }

        public Task UpdateAsync(AccidentInvolve obj)
        {
            throw new NotImplementedException();
        }
    }
}
