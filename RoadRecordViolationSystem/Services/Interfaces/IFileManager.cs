using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Services
{
   public interface IFileManager
    {
        Task UploadIFormFileAsync(IFormFile file,string path);

        void Delete(string path);
        string GeneratePath(IFormFile file,ExtensionType extensionType);
        public string GeneratePath(Image image);
        void UploadImage(Image image, string path);


        public enum ExtensionType
        {
            Video,
            Audio,
            Image,
            Pdf
        }
    }


}
