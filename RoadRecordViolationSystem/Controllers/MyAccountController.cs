using Microsoft.AspNetCore.Mvc;
using RoadRecordViolationSystem.Services.Interfaces;
using RoadRecordViolationSystem.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RoadRecordViolationSystem.Models;
using RoadRecordViolationSystem.Services;
using RoadRecordViolationSystem.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;

namespace RoadRecordViolationSystem.Controllers
{
    [Authorize]
    public class MyAccountController : Controller
    {
        #region private fieldss
        private readonly IUserOptions _passwordChanger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICrudRepository<UsersInformation> _userInfoService;
        private readonly IRoleProvider<ApplicationUser>  _roleService;
        private readonly IFileManager _imageManager;
        #endregion

        #region constructor dependency injection
        public MyAccountController(IUserOptions passwordChanger, UserManager<ApplicationUser> userManager, ICrudRepository<UsersInformation> userInfoService, IRoleProvider<ApplicationUser> roleService, IFileManager imageManager)
        {
            _passwordChanger = passwordChanger;
            _userManager = userManager;
            _userInfoService = userInfoService;
            _roleService = roleService;
            _imageManager = imageManager;
        }
        #endregion
        #region controller actions
        public async Task<IActionResult> MyAccountView()
        {
            var account = await _userManager.GetUserAsync(User);
            var info = await _userInfoService.GetByIdAsync(account.UsersInfoId);
            var profileModel = new MyProfileViewModel()
            {
                UsersInformation = info,
                Id = account.Id,
                ProfilePicture = account.ProfilePicture,
                Role =  _roleService.GetRoleName(
                    _roleService.GetRoleId(account.Id)
                    )
            };
            return View(profileModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> UpdateProfile(MyProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                // update user information
                await _userInfoService.UpdateAsync(model.UsersInformation);

                HttpContext.Session.SetString("UserFullName", $"{model.UsersInformation.GivenName} {model.UsersInformation.MiddleName[0]} {model.UsersInformation.LastName}");

                // return success signal
                return Json(new { isSuccess = true });
            }

            return View("/Views/MyAccount/MyAccountView",model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfilePicture(UpdatePictureViewModel model)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);

                // delete image
                _imageManager.Delete(model.ProfilePicture);

                // generate
                var path = _imageManager.GeneratePath(model.Picture, IFileManager.ExtensionType.Image);


                // copy image to wwwroot/images
                await _imageManager.UploadIFormFileAsync(model.Picture, path);

                // set content path
                user.ProfilePicture = path.ToImageContentPath();

                // update user account
                await _userManager.UpdateAsync(user);

                HttpContext.Session.SetString("ProfilePicture", user.ProfilePicture);

                return Ok();
            }

            catch(Exception ex)
            {
                return BadRequest(ex);
            }

            
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangeAddPasswordViewModel changedPasswordAccount)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (await _userManager.CheckPasswordAsync(currentUser, changedPasswordAccount
                    .AdminPassword))
                {
                    await _passwordChanger.ChangePasswordAsync(currentUser.UserName, changedPasswordAccount.Password);
                    return Json(new { IsSuccess = true }) ;
                }

                 ModelState.AddModelError("AdminPassword", "Invalid password");
                 return PartialView(changedPasswordAccount);

            }
                return PartialView(changedPasswordAccount);
        }
        #endregion
    }
}
