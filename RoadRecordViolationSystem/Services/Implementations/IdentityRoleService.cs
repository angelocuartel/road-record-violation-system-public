using Microsoft.AspNetCore.Identity;
using RoadRecordViolationSystem.Data;
using RoadRecordViolationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace RoadRecordViolationSystem.Services
{
    public class IdentityRoleService : IRoleProvider<ApplicationUser>
    { 
        private readonly AppDBContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public IdentityRoleService(AppDBContext context,RoleManager<IdentityRole> roleManager ,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public string GetRoleId(string userId)
        {
            return _context.UserRoles
              .Where(i => i.UserId == userId)
              .SingleOrDefault()
              .RoleId;
        }

        public string GetRoleName(string roleId)
        {
            return _roleManager.Roles.Where(i => i.Id == roleId)
                             .FirstOrDefault()
                             .Name;
        }

        public async Task UpdateRoleByUserIdAsync(string userId, string newRole)
        {
            var user = await GetUserAssociateToRoleAsync(userId);
            var prevUserRoleId = _context.UserRoles
                .Where(i => i.UserId == userId)
                .Select(i =>i.RoleId)
                .SingleOrDefault();
            var prevUserRole = (await _roleManager.FindByIdAsync(prevUserRoleId))
                                .Name;

            if (!prevUserRole.Equals(newRole))
            {
                await _userManager.RemoveFromRoleAsync(user, prevUserRole);
                await _userManager.AddToRoleAsync(user, newRole);
            }
        }
        
        private async Task<ApplicationUser> GetUserAssociateToRoleAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }
    }
}
