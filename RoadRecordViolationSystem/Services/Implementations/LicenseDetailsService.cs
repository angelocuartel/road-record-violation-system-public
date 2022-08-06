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
    public class LicenseDetailsService : ILicenseDetailsRepository<LicenseDetails>
    {
        private readonly AppDBContext _context;
        public LicenseDetailsService(AppDBContext context)
        {
            _context = context;
        }
        public async Task DeleteAsync(int id)
        {
            _context.Violations.Remove(await _context.Violations.FindAsync(id));
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<LicenseDetails>> GetAllAsync()
        {
            return await _context.LicenseDetails.ToListAsync();
        }

        public async Task<LicenseDetails> GetByIdAsync(int id)
        {
            return await _context.LicenseDetails.FindAsync(id);
        }

        public LicenseDetails GetByViolatorId(int id)
        {
            return _context.LicenseDetails.SingleOrDefault(i => i.ViolatorId == id);
        }

        public int GetLatestId()
        {
            throw new NotImplementedException();
        }

        public LicenseDetails GetLicenseByViolatorId(int id)
        {
            return _context.LicenseDetails.SingleOrDefault(i => i.ViolatorId == id);
        }

        public int GetLicenseDbID(string license)
        {

            return _context.LicenseDetails.Where(i => i.LicenseNo == license)
                    .Select(i => i.Id)
                    .FirstOrDefault();
        }

        public int GetRecordCount()
        {
            throw new NotImplementedException();
        }

        public async Task InsertAsync(LicenseDetails obj)
        {
            await _context.LicenseDetails.AddAsync(obj);
            await _context.SaveChangesAsync();
        }

        public async Task InsertAsync(List<LicenseDetails> objs)
        {
            await _context.LicenseDetails.AddRangeAsync(objs);
            await _context.SaveChangesAsync();
        }

        public bool LicenseExist(string license)
        {
            return _context.LicenseDetails.Where(i => i.LicenseNo == license).FirstOrDefault() != null;
        }

        public async Task UpdateAsync(LicenseDetails obj)
        {
            _context.LicenseDetails.Update(obj);
            await _context.SaveChangesAsync();
        }
    }
}
