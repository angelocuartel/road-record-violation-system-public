using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Services
{
   public interface IRoleProvider<T> where T : IdentityUser
    {
        string GetRoleId(string userId);

        string GetRoleName(string roleId);

        Task UpdateRoleByUserIdAsync(string userId,string newRole);
    }
}
