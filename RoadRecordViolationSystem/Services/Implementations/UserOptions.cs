using Microsoft.AspNetCore.Identity;
using RoadRecordViolationSystem.Models;
using RoadRecordViolationSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Services.Implementations
{
    public class UserOptions : IUserOptions
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserOptions(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task ChangePasswordAsync(string username, string newPassword)
        {
            var user = await _userManager.FindByNameAsync(username);
            var changePassToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            await _userManager.ResetPasswordAsync(user, changePassToken, newPassword);
        }
    }
}

