using RoadRecordViolationSystem.Data;
using RoadRecordViolationSystem.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Services.Implementations
{
    public class SettingService : ICrudRepository<Settings>
    {
        private readonly AppDBContext _context;
        public SettingService(AppDBContext context)
        {
            _context = context;
        }
        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Settings>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Settings> GetByIdAsync(int id)
        {
            return await _context.Settings.FindAsync(id);
        }

        public int GetLatestId()
        {
            throw new NotImplementedException();
        }

        public int GetRecordCount()
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(Settings obj)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Settings obj)
        {
             _context.Settings.Update(obj);
            await _context.SaveChangesAsync();

        }
    }
}
