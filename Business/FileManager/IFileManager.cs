using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.FileManager
{
    public interface IFileManager
    {
        IDataResult<CarImage> UploadImage(IFormFile file, CarImage carImage);
        IDataResult<CarImage> UpdateImage(IFormFile file, CarImage carImage);
        IDataResult<CarImage> DeleteImage(CarImage carImage);
    }
}