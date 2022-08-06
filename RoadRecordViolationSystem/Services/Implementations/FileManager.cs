using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Services
{
    public class FileManager : IFileManager
    {
        private readonly IWebHostEnvironment _host;
        public FileManager(IWebHostEnvironment host)
        {
            _host = host;
        }

        public void Delete( string path)
        {
            if (path != "~/profile.png")
            {
                File.Delete(Path.Combine(_host.WebRootPath, path.Substring(2)));
            }
        }


        public string GeneratePath(IFormFile file, IFileManager.ExtensionType extensionType)
        {
            var rootPath = _host.WebRootPath;
            var extension = Path.GetExtension(file.FileName);
            var noExtension = Path.GetFileNameWithoutExtension(file.FileName);
            var fileName = DateTime.UtcNow.ToString("yymmssfff") + noExtension + extension;
            var path = $"{rootPath}/Images/{fileName}";
            return extension == "" ? path + GetType(extensionType) : path;
        }

        public string GeneratePath(Image image)
        {
            var rootPath = _host.WebRootPath;
            var fileName = DateTime.UtcNow.ToString("yymmssfff");
            return $"{rootPath}/Images/{fileName}-signature.png";
        }



        public async Task UploadIFormFileAsync(IFormFile file,string path)
        {
                using (var stream = new FileStream( path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
        }

        public void UploadImage(Image image,string path)
        {
          image.Save(path);
        }


        private string GetType(IFileManager.ExtensionType extensionType)
        {
            switch ((int)extensionType)
            {
                case 0:
                    return ".mp4";
                case 1:
                    return ".mp3";
                case 2:
                    return ".png";
                case 3:
                    return ".pdf";
                default:
                    return "";
            }
        }

    }
}
