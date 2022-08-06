using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.ViewModels
{
    public class UpdatePictureViewModel
    {
        public IFormFile Picture { get; set; }
        public string    ProfilePicture { get; set; }
    }
}
