using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using RoadRecordViolationSystem.Data;
using RoadRecordViolationSystem.Models;
using RoadRecordViolationSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Services
{
    public class IdentityLogin : ILogProvider<LoginViewModel>
    {
        private readonly SignInManager<ApplicationUser> _signManager;
        private readonly IHttpContextAccessor _accessor;
        public IdentityLogin(SignInManager<ApplicationUser> signManager, IHttpContextAccessor accessor)
        {
            _signManager = signManager;
            _accessor = accessor;
        }
        public async Task<bool> IsAuthenticateAsync(LoginViewModel model)
        {
            var login = await _signManager.PasswordSignInAsync(model.UserName, model.PassWord, isPersistent: model.AlwaysMeIn, false);
            return login.Succeeded;
        }
        public async Task Logout()
        {
            await _signManager.SignOutAsync();
        }
    }
}
