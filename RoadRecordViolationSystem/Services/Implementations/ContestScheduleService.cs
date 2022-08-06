using Microsoft.EntityFrameworkCore;
using RoadRecordViolationSystem.Data;
using RoadRecordViolationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Services.Implementations
{
    public class ContestScheduleService : ICrudRepository<ContestSchedule>
    {
        private readonly AppDBContext _context;
        public ContestScheduleService(AppDBContext context)
        {
            _context = context;
        }
        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ContestSchedule>> GetAllAsync()
        {
            return await _context.ContestSchedules.AsNoTracking()
                .OrderByDescending(i => i.Id)
                .ToListAsync();
        }

        public Task<ContestSchedule> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public int GetLatestId()
        {
            throw new NotImplementedException();
        }

        public int GetRecordCount()
        {
            throw new NotImplementedException();
        }

        public async Task InsertAsync(ContestSchedule obj)
        {
            await _context.ContestSchedules.AddAsync(obj);
            await _context.SaveChangesAsync();
        }

        public Task UpdateAsync(ContestSchedule obj)
        {
            throw new NotImplementedException();
        }
    }
}
