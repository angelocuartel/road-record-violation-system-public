using Microsoft.EntityFrameworkCore;
using RoadRecordViolationSystem.Data;
using RoadRecordViolationSystem.Models;
using RoadRecordViolationSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Services
{
    public class UserInformationService : ICrudRepository<UsersInformation>
    {
        private readonly AppDBContext _context;
        public UserInformationService(AppDBContext context)
        {
            _context = context;
        }

        public async Task DeleteAsync(int id)
        {
            _context.Remove(
                await _context.UsersInformations
                .FindAsync(id));
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UsersInformation>> GetAllAsync()
        {
            return await _context.UsersInformations
                .OrderByDescending(i => i.UserInfoId)
                .ToListAsync();
        }

        public async Task<UsersInformation> GetByIdAsync(int id)
        {
            return await _context.UsersInformations
                .FindAsync(id);
        }

        public int GetLatestId()
        {
            throw new NotImplementedException();
        }

        public int GetRecordCount()
        {
            throw new NotImplementedException();
        }

        public async Task InsertAsync(UsersInformation obj)
        {
           await _context.UsersInformations
                    .AddAsync(obj);
           await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UsersInformation obj)
        {
            _context.UsersInformations
                    .Update(obj);
            await _context.SaveChangesAsync();
        }
    }
}
