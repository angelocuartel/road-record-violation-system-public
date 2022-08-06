using Microsoft.EntityFrameworkCore;
using RoadRecordViolationSystem.Data;
using RoadRecordViolationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Services.Implementations
{
    public class AccidentService : ICrudRepository<Accident>
    {
        private readonly AppDBContext _context;
        public AccidentService(AppDBContext context)
        {
            _context = context;
        }
        public async Task DeleteAsync(int id)
        {
            var accident = await _context.Accidents.FindAsync(id);
            _context.Accidents.Remove(accident);
          await  _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Accident>> GetAllAsync()
        {
            return await _context.Accidents.AsNoTracking()
                .OrderByDescending(i => i.Id)
                .ToListAsync();
        }

        public async Task<Accident> GetByIdAsync(int id)
        {
            return await _context.Accidents.FindAsync(id);
        }

        public int GetLatestId()
        {
            return _context.Accidents.OrderByDescending(i => i.Id).Select(i => i.Id).FirstOrDefault();
        }

        public int GetRecordCount()
        {
            return _context.Accidents.Count();
        }

        public async Task InsertAsync(Accident obj)
        {
            await _context.AddAsync(obj);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Accident obj)
        {
            _context.Accidents.Update(obj);
            await _context.SaveChangesAsync();
        }
    }
}
