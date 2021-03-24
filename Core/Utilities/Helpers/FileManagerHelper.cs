using System;
using System.IO;
using Core.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Core.Utilities.Helpers
{
    public  class FileManagerHelper
    {
       

        public static string Create(IFormFile file)
        {
            var imagePath = GetImagePath(file);
            var fullPath = GetFullPath(file,imagePath);
            file.Create(fullPath);
            return imagePath;
        }

        public static string Update(IFormFile file, string existingPath)
        {
            File.Delete(existingPath);
            var imagePath = GetImagePath(file);
            var fullPath = GetFullPath(file,imagePath);
            file.Create(fullPath);
            return imagePath;
        }

        public static void Delete(string extingPath)
        {
            File.Delete(extingPath);
        }

        private static string GetFullPath(IFormFile file,string imagePathGuid)
        {
            var path = Environment.CurrentDirectory+ @"\wwwroot\images";
            var fullPath = Path.Combine(path, imagePathGuid);
            return fullPath;
        }

        private static string GetImagePath(IFormFile file)
        {
            var imagePath = Guid.NewGuid() + Path.GetExtension(file.FileName);
            return imagePath;
        }
    }
}