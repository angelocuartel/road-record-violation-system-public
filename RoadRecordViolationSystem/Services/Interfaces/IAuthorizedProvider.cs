using Microsoft.AspNetCore.Identity;
using RoadRecordViolationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Services
{
   public interface IAuthorizedProvider<T> where T : IdentityUser
    {
        Task<string> GetAuthLayoutAsync(T user, string role);
        string GetAuthView();
    }
}
