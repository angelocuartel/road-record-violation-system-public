using Microsoft.AspNetCore.Http;
using RoadRecordViolationSystem.Models;


namespace RoadRecordViolationSystem.ViewModels
{
    public class AddUserViewModel
    {
        public AccountListViewModel Account { get; set; }
        public UsersInformation UserInfo { get; set; }
        public IFormFile ImageFile { get; set; }
        public string Password { get; set; }
        public string  ConfirmPassword { get; set; }
    }
}
