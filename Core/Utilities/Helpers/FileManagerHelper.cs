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
            var fullPath = FullPath(file);
            file.Create(fullPath);
            return fullPath;
        }

        public static string Update(IFormFile file, string existingPath)
        {
            File.Delete(existingPath);
            var fullPath = FullPath(file);
            file.Create(fullPath);
            return fullPath;
        }

        public static void Delete(string path)
        {
            File.Delete(path);
        }

        private static string FullPath(IFormFile file)
        {
            var path = Environment.CurrentDirectory+ @"\wwwroot\images";
            var imagePath = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var fullPath = Path.Combine(path, imagePath);
            return fullPath;
        }
    }
}