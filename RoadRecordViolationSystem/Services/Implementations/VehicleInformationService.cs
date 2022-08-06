using Microsoft.EntityFrameworkCore;
using RoadRecordViolationSystem.Data;
using RoadRecordViolationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Services.Implementations
{
    public class VehicleInformationService : ICrudRepository<VehicleInformation>
    {
        private readonly AppDBContext _context;
        public VehicleInformationService(AppDBContext context)
        {
            _context = context;
        }
        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<VehicleInformation>> GetAllAsync()
        {
            return await _context.vehicleInformations.AsNoTracking()
                .OrderByDescending(i => i.Id)
                .ToListAsync();
        }

        public int GetByIdAsync()
        {
          return _context.vehicleInformations.OrderByDescending(i => i.Id)
                .Select(i => i.Id)
                .SingleOrDefault();
        }

        public async Task<VehicleInformation> GetByIdAsync(int id)
        {
            return await _context.vehicleInformations.FindAsync(id);
        }

        public int GetLatestId()
        {
          return  _context.vehicleInformations.OrderByDescending(i => i.Id)
                .Select(i => i.Id)
                .FirstOrDefault();
            
        }

        public int GetRecordCount()
        {
            throw new NotImplementedException();
        }

        public async Task InsertAsync(VehicleInformation obj)
        {
           await _context.vehicleInformations.AddAsync(obj);
           await _context.SaveChangesAsync();
        }

        public Task UpdateAsync(VehicleInformation obj)
        {
            throw new NotImplementedException();
        }
    }
}
