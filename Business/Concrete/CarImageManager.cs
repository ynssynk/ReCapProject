﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        private ICarImageDal _carImageDal;
        
        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
            
        }
        [CacheAspect]
        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }

        [CacheAspect()]
        public IDataResult<CarImage> Get(int id)
        {
            var result = _carImageDal.Get(c => c.Id == id);
            result.ImagePath = result.ImagePath.Split(new string[] { "\\" }, StringSplitOptions.None).LastOrDefault();
            return new SuccessDataResult<CarImage>(result);
        }
        [CacheRemoveAspect("ICarImageService.Get")]
        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Add(IFormFile file, CarImage carImage)
        {

            var result = BusinessRules.Run(CheckIfFileIsEmpty(file), CheckIfCarImagesCountLimit(carImage.CarId), CheckIfExtensionsAreAllowed(file));
            if (result != null)
            {
                return result;
            }

            var addedImage = new CarImage
            {
                CarId = carImage.CarId,
                Date = DateTime.Now,
                ImagePath = FileManagerHelper.Create(file)
            };

            //var resultImage=_fileManager.UploadImage(file, carImage);
            _carImageDal.Add(addedImage);

            return new SuccessResult(Messages.Success);
        }
        [CacheRemoveAspect("ICarImageService.Get")]
        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Update(IFormFile file, CarImage carImage)
        {
            var result = BusinessRules.Run(CheckIfFileIsEmpty(file), CheckIfExtensionsAreAllowed(file));
            if (result != null)
            {
                return result;
            }

            var updatedImage = _carImageDal.Get(c => c.Id == carImage.Id);
            updatedImage.CarId = carImage.CarId;
            var existingPath = ExistingPath(updatedImage.ImagePath);
            updatedImage.ImagePath = FileManagerHelper.Update(file, existingPath);
            updatedImage.Date = DateTime.Now;
            
            _carImageDal.Update(updatedImage);
            return new SuccessResult(Messages.Success);

        }
        [CacheRemoveAspect("ICarImageService.Get")]
        public IResult Delete(CarImage carImage)
        {

            var deletedImage = _carImageDal.Get(c => c.Id == carImage.Id);
            var existingPath = ExistingPath(deletedImage.ImagePath);
            FileManagerHelper.Delete(existingPath);
            _carImageDal.Delete(deletedImage);
            return new SuccessResult(Messages.Success);
        }

        

        //[CacheAspect()]
        public IDataResult<List<CarImage>> GetImagesByCarId(int carId)
        {
            var result = BusinessRules.Run(CheckIfCarImageNull(carId));

            if (result != null)
            {
                return new ErrorDataResult<List<CarImage>>(result.Message);
            }
            return new SuccessDataResult<List<CarImage>>(CheckIfCarImageNull(carId).Data);
        }
        private static string ExistingPath(string existingPath)
        {
            return Path.Combine(Environment.CurrentDirectory + @"\wwwroot\images\" + existingPath);
        }
        private IResult CheckIfFileIsEmpty(IFormFile file)
        {
            if (file.Length < 0)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }
        private IResult CheckIfCarImagesCountLimit(int carId)
        {
            var result = _carImageDal.GetAll(c => c.CarId == carId).Count;
            if (result > 5)
            {
                return new ErrorResult(Messages.CarImagesCountLimit);
            }
            return new SuccessResult();
        }

        private IResult CheckIfExtensionsAreAllowed(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName);
            string[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg" };
            if (allowedExtensions.Contains(extension.ToLower()))
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        private IDataResult<List<CarImage>> CheckIfCarImageNull(int carId)
        {
            string defaultPath = Environment.CurrentDirectory + @"\wwwroot\images\default.png";

            var result = _carImageDal.GetAll(c => c.CarId == carId).Any();
            if (!result)
            {
                var carImages = new List<CarImage>();
                carImages.Add(new CarImage { CarId = carId, ImagePath = defaultPath, Date = DateTime.Now });
                carImages.ForEach(i => i.ImagePath = i.ImagePath.Split(new string[] { "\\" }, StringSplitOptions.None).LastOrDefault());
                return new SuccessDataResult<List<CarImage>>(carImages);
            }
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(c => c.CarId == carId));
        }

    }
}