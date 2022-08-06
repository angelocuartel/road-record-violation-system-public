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
    public class ViolationTypesService : IViolationTypesRepository<ViolationTypes>
    {

        private readonly AppDBContext _context;
        public ViolationTypesService(AppDBContext context)
        {
            _context = context;
        }


        public async Task DeleteAsync(int id)
        {
            var vioType = _context.ViolationTypes
                .Where(i => i.Id == id)
                .FirstOrDefault();

            _context.Remove(vioType);
           await _context.SaveChangesAsync();
        }

        public bool Exist(string violationName)
        {
            return  _context.ViolationTypes
                .Where(i => i.Name.ToLower() == violationName.ToLower())
                .FirstOrDefault()
                != null;
        }

        public async Task<IEnumerable<ViolationTypes>> GetAllAsync()
        {
            return await _context.ViolationTypes.OrderBy(i => i.Name)
                .AsNoTracking()
                .OrderByDescending(i => i.Name)
                .ToListAsync();
        }

        public async Task<ViolationTypes> GetByIdAsync(int id)
        {
            return await _context.ViolationTypes.FindAsync(id);
        }

        public int GetLatestId()
        {
            throw new NotImplementedException();
        }

        public int GetRecordCount()
        {
            throw new NotImplementedException();
        }

        public async Task InsertAsync(ViolationTypes obj)
        {
            obj.Name = obj.Name.ToUpper();
           await _context.ViolationTypes.AddAsync(obj);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ViolationTypes obj)
        {
            obj.Name = obj.Name.ToUpper();
            _context.ViolationTypes.Update(obj);
          await _context.SaveChangesAsync();
        }
    }
}
