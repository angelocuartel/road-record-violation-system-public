
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RoadRecordViolationSystem.Models;
using RoadRecordViolationSystem.Services;
using RoadRecordViolationSystem.Services.Interfaces;
using RoadRecordViolationSystem.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RoadRecordViolationSystem.Helpers;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace RoadRecordViolationSystem.Controllers
{
    [Authorize(Roles ="Administrator")]
    public class AdminAccountsController : Controller
    {
        #region fields
        private readonly ICrudRepository<UsersInformation> _userInfoService;
        private readonly IFileManager _imageService;
        private readonly IRoleProvider<ApplicationUser> _userRoleService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserOptions _passwordChanger;
        private string _partialPath = "/Views/AdminAccounts/_AddUserFormPartial.cshtml";
        #endregion fields

        #region constructor
        public AdminAccountsController( ICrudRepository<UsersInformation> userInforService,IFileManager imageService, UserManager<ApplicationUser> userManager, IRoleProvider<ApplicationUser> userRoleService, SignInManager<ApplicationUser> signInManager, IUserOptions passwordChanger)
        {
            _userInfoService = userInforService;
            _imageService = imageService;
            _userManager = userManager;
            _userRoleService = userRoleService;
            _signInManager = signInManager;
            _passwordChanger = passwordChanger;
        }
        #endregion constructor

        #region controller actions
        [HttpGet]
        public async  Task<IActionResult> AccountList()
        {
            return View(await GetAccountListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetFormPartial(AccountListViewModel userAccount)
         {
            if (userAccount.AccountId == null || userAccount.InfoId == 0)
            {
                return PartialView(_partialPath);
            }
                var hashedPassword = await _userManager.FindByIdAsync(userAccount.AccountId);
                var viewModel = new AddUserViewModel()
                {
                    Account = userAccount,
                    UserInfo = await _userInfoService.GetByIdAsync(userAccount.InfoId),
                    Password =  hashedPassword.PasswordHash,
                    ConfirmPassword = hashedPassword.PasswordHash
                };

                return PartialView(_partialPath, viewModel);
        }

        [HttpGet]
        public IActionResult GetChangePassPartial(string userName)
        {
            var account = new ChangeAddPasswordViewModel()
            {
                UserName = userName
            };

            return PartialView("/Views/AdminAccounts/_ChangePasswordPartial.cshtml", account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangeAddPasswordViewModel changedPasswordAccount)
        {
            var partialPath = "/Views/AdminAccounts/_ChangePasswordPartial.cshtml";

            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);

                if (await _userManager.CheckPasswordAsync(currentUser, changedPasswordAccount.AdminPassword))
                {
                   await _passwordChanger.ChangePasswordAsync(currentUser.UserName, changedPasswordAccount.Password);
                    return RedirectToAction(nameof(GetTablePartial));
                }

                ModelState.AddModelError("AdminPassword", "Invalid Password");
                return PartialView(partialPath, changedPasswordAccount);
            }

            return PartialView(partialPath,changedPasswordAccount);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddUserViewModel userAccount)
        {
            if (ModelState.IsValid)
            {
                if (userAccount.ImageFile != null)
                {
                    var imagePath = _imageService.GeneratePath(userAccount.ImageFile,IFileManager.ExtensionType.Image);
                    await _imageService.UploadIFormFileAsync(userAccount.ImageFile, imagePath);
                    userAccount.Account.ProfilePicture = imagePath.ToImageContentPath();
                }
                else
                {
                    userAccount.Account.ProfilePicture = "~/profile.png";
                }
           
                await _userInfoService.InsertAsync(userAccount.UserInfo);

                var applicationUser = new ApplicationUser()
                {
                    UserName = userAccount
                    .Account
                    .UserName,
                    UsersInfoId =  _userInfoService.GetAllAsync()
                                    .Result
                                    .OrderByDescending(i => i.UserInfoId)
                                    .Select(i => i.UserInfoId)
                                    .FirstOrDefault(),
                    ProfilePicture = userAccount.Account.ProfilePicture,
                    AccountStatus = "ACTIVE"
                };

                await _userManager.CreateAsync(applicationUser, userAccount
                    .Password);
            await _userManager.AddToRoleAsync(applicationUser, userAccount
                .Account
                .Role);
                return RedirectToAction(nameof(GetTablePartial));
            }

            return PartialView(_partialPath, userAccount);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(AddUserViewModel userAccount)
        {
            if (ModelState.IsValid)
            {
                if (userAccount.ImageFile != null)
                {
                    var imagePath = _imageService.GeneratePath(userAccount.ImageFile, IFileManager.ExtensionType.Image);
                    await _imageService.UploadIFormFileAsync(userAccount.ImageFile, imagePath);
                    _imageService.Delete(userAccount.Account.ProfilePicture);
                    userAccount.Account.ProfilePicture = imagePath.ToImageContentPath();
                }

                await _userInfoService.UpdateAsync(userAccount.UserInfo);
                var account = await _userManager.FindByIdAsync(userAccount.Account.AccountId);
                account.UserName = userAccount.Account.UserName;
                account.ProfilePicture = userAccount.Account.ProfilePicture;
                await _userManager.UpdateAsync(account);
                await _userRoleService.UpdateRoleByUserIdAsync(userAccount.Account.AccountId, userAccount.Account.Role);

              return RedirectToAction(nameof(GetTablePartial));
            }

            return PartialView(_partialPath, userAccount);
        }

        [HttpGet]
        public async  Task<IActionResult> GetTablePartial()
        {
            return PartialView("/Views/AdminAccounts/_AccountTablePartial.cshtml",await GetAccountListAsync());
        }

        [HttpGet]
        public IActionResult GetDeletePartial(string userName, int usersInfo)
        {
            var user = new ApplicationUser()
            {
                UserName = userName,
                UsersInfoId = usersInfo
            };

            return PartialView(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id,string userName)
        {
            var account = await _userManager.FindByNameAsync(userName);
            _imageService.Delete(account.ProfilePicture);
            await _userInfoService.DeleteAsync(id); 

            return RedirectToAction(nameof(GetTablePartial));
        }
        
        [HttpGet]
        public IActionResult GetArchivePartial(ArchiveUserViewModel user)
        {
            return PartialView(user);
        }

        [HttpPost]
  
        public async  Task<IActionResult> ArchiveActivateUser(ArchiveUserViewModel user)
        {
            var account = await _userManager.FindByNameAsync(user.UserName);
            account.AccountStatus = user.Status.Equals("ACTIVE") ? "ARCHIVE" : "ACTIVE";
            await _userManager.UpdateAsync(account);

            return RedirectToAction(nameof(GetTablePartial));
        }
        #endregion controller actions

        #region private methods
        private Task<List<AccountListViewModel>> GetAccountListAsync()
        {
           return Task.Run(() =>
            {
                var accList = _userManager.Users;

                var newUserList = accList.Select(m => new AccountListViewModel()
                {

                    FullName = $"{m.UsersInformation.LastName}, {m.UsersInformation.GivenName} {m.UsersInformation.MiddleName}",
                    UserName = m.UserName,
                    AccountId = m.Id,
                    InfoId = m.UsersInfoId,
                    Role = _userRoleService.GetRoleName(_userRoleService.GetRoleId(m.Id)),
                    ProfilePicture = m.ProfilePicture,
                    Status = m.AccountStatus
                })
                .Where(i => !i.UserName
                .Equals(User.Identity.Name))
                .OrderByDescending(i => i.InfoId)
                .ToList();

                return newUserList;
            });
        }
    }
    #endregion private methods
}
