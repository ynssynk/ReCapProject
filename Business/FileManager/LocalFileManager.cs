using System;
using System.IO;
using Business.Abstract;
using Business.FileManager;
using Core.Extensions;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace Business.Storages
{
    public class LocalFileManager:IFileManager
    {
        public IDataResult<CarImage> UploadImage(IFormFile file, CarImage carImage)
        {

            var addedImage = new CarImage
            {
                Id = carImage.Id, CarId = carImage.CarId, ImagePath = FileManagerHelper.Create(file),
                Date = DateTime.Now
            };
            return new SuccessDataResult<CarImage>(addedImage);
        }
        public IDataResult<CarImage> UpdateImage(IFormFile file, CarImage carImage)
        {
            var updatedImage = new CarImage
            {
                Id = carImage.Id, CarId = carImage.CarId,
                ImagePath = FileManagerHelper.Update(file, carImage.ImagePath), Date = DateTime.Now
            };
            return new SuccessDataResult<CarImage>(updatedImage);
        }

        public IDataResult<CarImage> DeleteImage(CarImage carImage)
        {
            FileManagerHelper.Delete(carImage.ImagePath);
            return new SuccessDataResult<CarImage>(carImage);
        }
        
    }
}