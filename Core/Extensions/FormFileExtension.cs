using System.IO;
using Microsoft.AspNetCore.Http;

namespace Core.Extensions
{
    public static class FormFileExtension
    {
        public static void Create(this IFormFile file, string path)
        {
            file.CopyTo(new FileStream(path,FileMode.Create));
        }
    }
}