using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Business.Abstract;
using Business.Constants;
using Business.Storages;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
namespace Business.Concrete
{
    public class CarImageManager:ICarImageService
    {
        private ICarImageDal _carImageDal;
        
        private IStorage _storage;
        public CarImageManager(ICarImageDal carImageDal, IStorage storage)
        {
            _carImageDal = carImageDal;
            _storage = storage;
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            throw new System.NotImplementedException();
        }


        public IDataResult<CarImage> Get(int id)
        {
            var result= _carImageDal.Get(c => c.Id == id);
            return new SuccessDataResult<CarImage>(result);
        }

        public IResult Add(IFormFile file,  CarImage carImage)
        {
            if (file.Length<0)
            {
                return new ErrorResult();
            }
            var result = BusinessRules.Run(CheckIfCarImagesCountLimit(carImage.CarId),CheckIsFormatImage(file));
            if (result!=null)
            {
                return result;
            }
            var storageResults= _storage.UploadImage(file, carImage);

            _carImageDal.Add(storageResults.Data);
            return new SuccessResult(Messages.Success);
        }

        public IResult Update(IFormFile file,CarImage carImage)
        {
            var result = BusinessRules.Run(CheckIsFormatImage(file));
            if (result!=null)
            {
                return result;
            }
            var resultImage= _carImageDal.Get(c => c.Id == carImage.Id);
            var storageUpdate = _storage.UpdateImage(file, resultImage);
             _carImageDal.Update(storageUpdate.Data);
             return new SuccessResult(Messages.Success);

        }

        public IResult Delete(CarImage carImage)
        {

            var deletedImage = _carImageDal.Get(c => c.Id == carImage.Id);
            var resultStorage= _storage.DeleteImage(deletedImage);
            _carImageDal.Delete(resultStorage.Data);
            return new SuccessResult(Messages.Success);
        }

        private IResult CheckIfCarImagesCountLimit(int carId)
        {
            var result = _carImageDal.GetAll(c => c.CarId == carId).Count;
            if (result>5)
            {
                return new ErrorResult(Messages.CarImagesCountLimit);
            }

            return new SuccessResult();
        }

        private IResult CheckIsFormatImage(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName);
            string[] allowedExtensions = {".gif", ".png", ".jpeg", ".jpg"};
            if (allowedExtensions.Contains(extension.ToLower()))
            {
                return new SuccessResult();
            }
            
            return new ErrorResult();
        }

    }
}