using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RoadRecordViolationSystem.Models;
using RoadRecordViolationSystem.Services;
using RoadRecordViolationSystem.Services.Interfaces;
using RoadRecordViolationSystem.ViewModels;
using System;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Controllers
{
    public class LogController : Controller
    {
        #region private fields
        private readonly ILogProvider<LoginViewModel> _login;
        private readonly IAuthorizedProvider<ApplicationUser> _auth;
        private readonly UserManager<ApplicationUser> _user;
        private readonly IlogHistory _historyService;
        private readonly SignInManager<ApplicationUser> _signIn;
        private readonly ICrudRepository<UsersInformation> _userService;
        #endregion

        #region constructor dependency injection
        public LogController(ILogProvider<LoginViewModel> login, IAuthorizedProvider<ApplicationUser> auth, UserManager<ApplicationUser> user,IlogHistory historyService,SignInManager<ApplicationUser> signIn
            , ICrudRepository<UsersInformation> userService)
        {
            _login = login;
            _auth = auth;
            _user = user;
            _historyService = historyService;
            _signIn = signIn;
            _userService = userService;

        }
        #endregion

        #region controller actions

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> SystemLogin()
        {
  
            if (_signIn.IsSignedIn(User))
            {
                var account = await _user.FindByNameAsync(User.Identity.Name);
                ViewBag.AuthLayout = await _auth.GetAuthLayoutAsync(account, "Enforcer");

                var userInfo =await _userService.GetByIdAsync(account.UsersInfoId);

                HttpContext.Session.SetString("UserFullName", $"{userInfo.GivenName} {userInfo.MiddleName}. {userInfo.LastName}");
                HttpContext.Session.SetString("ProfilePicture", account.ProfilePicture);

                return View(_auth.GetAuthView());
            }
            else
            {

                return View();

            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> SystemLogin(LoginViewModel credentials)
        {
            ViewBag.DisableLogin = null;

            if (ModelState.IsValid)
            {
                var account = await _user.FindByNameAsync(credentials.UserName);

                if (account != null && account.ValidTimeToLogin != null && DateTime.Now.TimeOfDay >= account.ValidTimeToLogin.Value.TimeOfDay)
                {
                    account.AccessFailedCount = 0;
                    account.ValidTimeToLogin = null;
                    await _user.UpdateAsync(account);
                    ViewBag.DisableLogin = false;
                }
                else if (account != null && account.AccessFailedCount == 5)
                {
                    ViewBag.ErrorLogin = "You have attempted 5 invalid logins please wait for 5 minutes Thank you!";
                    ViewBag.DisableLogin = true;
                    account.ValidTimeToLogin =  DateTime.Now.AddMinutes(5);
                    await _user.UpdateAsync(account);

                    return View();
                }
                 if (account != null && account.AccountStatus == "ARCHIVE")
                {
                    ViewBag.ErrorLogin = "Your Account is Deactivated please contact Primary Administrator Thank you !";
                    return View();
                }

                else if (await _login.IsAuthenticateAsync(credentials) && account.AccountStatus == "ACTIVE")
                {
                    ViewBag.AuthLayout = await _auth.GetAuthLayoutAsync(account, "Enforcer");

                    DateTime dateNow = DateTime.Now;
                    
                    await _historyService.RecordlogInAsync(account.Id,dateNow.ToLongDateString(),dateNow.ToLongTimeString());

                    HttpContext.Session.SetString("DateIn", dateNow.ToLongDateString());
                    HttpContext.Session.SetString("TimeIn", dateNow.ToLongTimeString());
                    HttpContext.Session.SetString("UserLogId", account.Id);

                    return RedirectToAction(nameof(ShowAauthView), new { viewPath = _auth.GetAuthView(),userId = account.UsersInfoId,profilePicture = account.ProfilePicture });
                }
                else if (!await _login.IsAuthenticateAsync(credentials))
                {
                    ViewBag.ErrorLogin = "Invalid Username or password";

                    if (account != null)
                    {
                        account.AccessFailedCount += 1;
                        await _user.UpdateAsync(account);
                    }

                    return View();
                }
            }
            return View(credentials);
        }
        [AllowAnonymous]
        public async Task<IActionResult> ShowAauthView(string viewPath,int userId, string profilePicture)
        {
            var userInfo = await _userService.GetByIdAsync(userId);

            HttpContext.Session.SetString("UserFullName", $"{userInfo.GivenName} {userInfo.MiddleName}. {userInfo.LastName}");
            HttpContext.Session.SetString("ProfilePicture", profilePicture);

            return View(viewPath);
        }

        [Authorize]
        public async Task<IActionResult> Logout(ApplicationUser user)
        {
            await _login.Logout();
            await _historyService.RecordLogOutAsync();

            HttpContext.Session.Clear();
            return RedirectToAction(nameof(SystemLogin));

            
        }
        #endregion
    }
}
