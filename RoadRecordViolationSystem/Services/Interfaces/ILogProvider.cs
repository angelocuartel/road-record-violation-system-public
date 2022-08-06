using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Services
{
    public interface ILogProvider <T> where T : class
    { 
        Task<bool> IsAuthenticateAsync(T obj);
        Task Logout();
    }
}
