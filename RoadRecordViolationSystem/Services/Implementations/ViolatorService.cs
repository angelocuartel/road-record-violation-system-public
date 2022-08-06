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
    public class ViolatorService : IViolatorRepository<Violator>
    {
        private readonly AppDBContext _context;
        public ViolatorService(AppDBContext context)
        {
            _context = context;
        }
        public async Task DeleteAsync(int id)
        {
           var obj = await _context.Violators.FindAsync(id);
             _context.Violators.Remove(obj);
          await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Violator>> GetAllAsync()
        {
            return await _context.Violators
                .OrderByDescending(i => i.Id)
                .ToListAsync();
        }

        public  bool NameExist(string fullName)
        {
            var nameSections = fullName.ToUpper().Trim().Split('-');

            return  _context.Violators.Where(i => i.GivenName.ToUpper() == nameSections[0] && i.MiddleName.ToUpper() == nameSections[1] && i.LastName.ToUpper() == nameSections[2])
                .FirstOrDefault() != null;
        }

        public async Task<Violator> GetByIdAsync(int id)
        {
            return await _context.Violators.FindAsync(id);
        }

        public async Task InsertAsync(Violator obj)
        {
           await _context.AddAsync(obj);
           await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Violator obj)
        {
            _context.Violators.Update(obj);
            await _context.SaveChangesAsync();
        }

        public int GetDbIdByName(string fullname)
        {
            var nameSections = fullname.ToUpper().Trim().Split('-');

            return _context.Violators.Where(i => i.GivenName.ToUpper() == nameSections[0] && i.MiddleName.ToUpper() == nameSections[1] && i.LastName.ToUpper() == nameSections[2])
                .Select(i => i.Id)
                .FirstOrDefault();
        }

        public int GetLatestId()
        {
           return _context.Violators.OrderByDescending(i => i.Id)
                 .Select(i => i.Id)
                 .FirstOrDefault();
        }

        public int GetRecordCount()
        {
            return _context.Violators.Count();
        }
    }
}
