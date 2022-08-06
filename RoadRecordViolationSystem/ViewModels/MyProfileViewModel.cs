
using Microsoft.AspNetCore.Http;
using RoadRecordViolationSystem.Models;

namespace RoadRecordViolationSystem.ViewModels
{
    public class MyProfileViewModel 
    {
        public UsersInformation UsersInformation { get; set; }
        public string ProfilePicture { get; set; }
        public string Id { get; set; }
        public string Role { get; set; }
    }
}
