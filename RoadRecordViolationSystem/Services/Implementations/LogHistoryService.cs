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
    public class LogHistoryService : ILogHistoryProvider<UsersLogHistory>
    {
        private readonly AppDBContext _context;
        public LogHistoryService(AppDBContext context)
        {
            _context = context;
        }

        public async Task DeleteAllAsync(string id)
        {
            var histories = await _context.UsersLogHistories
                .Where(i => i.Id == id && i.DateOut != "--")
                .ToListAsync();

            _context.UsersLogHistories.RemoveRange(histories);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int logId)
        {
            var log = _context.UsersLogHistories.Find(logId);
            _context.UsersLogHistories.Remove(log);
           await _context.SaveChangesAsync();
        }

        public async  Task<List<UsersLogHistory>> GetLogHistoriesAsync()
        {
            var loghistories = await _context.UsersLogHistories
                .OrderByDescending(d => d.LogId)
                .ToListAsync();

            foreach (var logHistory in loghistories)
            {
                logHistory.User = await _context.Users
                    .FirstOrDefaultAsync(i => i.Id == logHistory.Id);
            }
            return loghistories;
        }
    }
}
