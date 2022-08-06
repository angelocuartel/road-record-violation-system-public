using Microsoft.AspNetCore.Http;
using RoadRecordViolationSystem.Data;
using RoadRecordViolationSystem.Models;
using RoadRecordViolationSystem.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Services.Implementations
{
    public class LogHistory : IlogHistory 
    {
       private AppDBContext _context;
       private readonly IHttpContextAccessor _accessor;
        
        public LogHistory(AppDBContext context,IHttpContextAccessor accessor)
        {
            _context = context;
            _accessor = accessor;
        }

        public async Task RecordlogInAsync(string id,string dateIn, string timeIn)
        {
            var log = new UsersLogHistory()
            {
                Id = id,
                TimeIn = timeIn,
                DateIn = dateIn,
                TimeOut = "--",
                DateOut = "--",

            };


            await _context.UsersLogHistories.AddAsync(log);
            await _context.SaveChangesAsync();
        }

        public async Task RecordLogOutAsync()
        {
            var recentLog = _context.UsersLogHistories
                             .Where(t => t.Id == _accessor.HttpContext.Session.GetString("UserLogId") &&
                             t.TimeIn == _accessor.HttpContext.Session.GetString("TimeIn") && 
                             t.DateIn == _accessor.HttpContext.Session.GetString("DateIn"))
                             .SingleOrDefault();

            recentLog.TimeOut = DateTime.Now
                .ToLongTimeString();

            recentLog.DateOut = DateTime.Today
                .ToLongDateString();

            _context.UsersLogHistories.Update(recentLog);
            await _context.SaveChangesAsync();
        }
    }
}
