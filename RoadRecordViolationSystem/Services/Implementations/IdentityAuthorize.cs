using Microsoft.AspNetCore.Identity;
using RoadRecordViolationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Services
{
    public class IdentityAuthorize : IAuthorizedProvider<ApplicationUser>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private bool _isInRole = false;
        public IdentityAuthorize(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<string> GetAuthLayoutAsync(ApplicationUser user, string role)
        {

           _isInRole = await _userManager.IsInRoleAsync(user, "Enforcer");

            return _isInRole ?  "/Views/Shared/_EnforcerLayout.cshtml" : "/Views/Shared/ _NonEnforcerLayout.cshtml";
        }

        public string GetAuthView()
        {
            return _isInRole ? "/Views/Dashboard/ScanQR.cshtml" : "/Views/Dashboard/GetDashboardView.cshtml";
        }
    }
}
