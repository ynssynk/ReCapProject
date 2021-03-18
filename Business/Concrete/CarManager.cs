using System;
using System.Collections.Generic;
using System.Linq;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;


namespace Business.Concrete
{

    public class CarManager : ICarService
    {
        private ICarDal _carDal;
        private ICarImageService _carImageService;
        public CarManager(ICarDal carDal, ICarImageService carImageService)
        {
            _carDal = carDal;
            _carImageService = carImageService;
        }
        [CacheAspect()]
        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll());
        }
        [CacheAspect()]
        public IDataResult<Car> Get(int carId)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.Id == carId));
        }
        [CacheRemoveAspect("ICarService.Get")]
        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car car)
        {

            var result = BusinessRules.Run(CheckDailyPrice(car.DailyPrice));
            if (result != null)
            {
                return result;
            }

            _carDal.Add(car);
            return new SuccessResult(Messages.CarAdded);
        }
        [CacheRemoveAspect("ICarService.Get")]
        [ValidationAspect(typeof(CarValidator))]
        public IResult Update(Car car)
        {

            _carDal.Update(car);
            return new SuccessResult(Messages.CarUpdated);

        }
        [CacheRemoveAspect("ICarService.Get")]
        [ValidationAspect(typeof(CarValidator))]
        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.CarDeleted);
        }
        [CacheAspect()]
        public IDataResult<List<Car>> GetCarsByBrandId(int brandId)
        {

            return new SuccessDataResult<List<Car>>(_carDal.GetAll(b => b.BrandId == brandId));
        }
        
        [CacheAspect()]
        public IDataResult<List<Car>> GetCarsByColorId(int colorId)
        {

            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == colorId));
        }
        //[SecuredOperation("car.getcardetail,admin")]
        [CacheAspect()]
        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            var carDetailDtos = _carDal.GetCarDetails();
            var result = BusinessRules.Run(CheckIfImageIsEmpty(carDetailDtos));
            if (result!=null)
            {
                return new ErrorDataResult<List<CarDetailDto>>(Messages.Error);
            }
            return new SuccessDataResult<List<CarDetailDto>>(carDetailDtos);
        }
        [CacheAspect()]
        public IDataResult<List<CarDetailDto>> GetCarDetailsById(int carId)
        {
            var carDetailDto = _carDal.GetCarDetails(c => c.Id == carId);
            var result = BusinessRules.Run(CheckIfImageIsEmpty(carDetailDto));
            if (result!=null)
            {
                return new ErrorDataResult<List<CarDetailDto>>(Messages.Error);
            }

            return new SuccessDataResult<List<CarDetailDto>>(carDetailDto);
        }
        [CacheAspect()]
        public IDataResult<List<CarDetailDto>> GetCarsDetailsByBrandId(int brandId)
        {
            var carDetailsBrandIdDtos = _carDal.GetCarDetails(c => c.BrandId == brandId);
            var result = BusinessRules.Run(CheckIfImageIsEmpty(carDetailsBrandIdDtos));
            if (result!=null)
            {
                return new ErrorDataResult<List<CarDetailDto>>(Messages.Error);
            }

            return new SuccessDataResult<List<CarDetailDto>>(carDetailsBrandIdDtos);
        }
        [CacheAspect()]
        public IDataResult<List<CarDetailDto>> GetCarsDetailsByColorId(int colorId)
        {
            var carDetailsColorIdDtos = _carDal.GetCarDetails(c => c.ColorId == colorId);
            var result = BusinessRules.Run(CheckIfImageIsEmpty(carDetailsColorIdDtos));
            if (result!=null)
            {
                return new ErrorDataResult<List<CarDetailDto>>(Messages.Error);
            }

            return new SuccessDataResult<List<CarDetailDto>>(carDetailsColorIdDtos);
        }
        private IDataResult<List<CarDetailDto>> CheckIfImageIsEmpty(List<CarDetailDto> carDetailDtos)
        {
            var result = carDetailDtos;
            foreach (var carDetailDto in result)
            {
                if (carDetailDto.ImagePath == null)
                {
                    var resultCarImage = _carImageService.GetImagesByCarId(carDetailDto.Id);
                    var carImages = resultCarImage.Data;
                    foreach (var carImage in carImages)
                    {
                        carDetailDto.Id = carImage.CarId;
                        carDetailDto.ImagePath = carImage.ImagePath;
                    }
                }
            }
            return new SuccessDataResult<List<CarDetailDto>>(result);
        }

        [CacheAspect()]
        public IDataResult<Car> GetById(int id)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.Id == id));
        }
        private IResult CheckDailyPrice(decimal dailyPrice)
        {
            if (dailyPrice < 0)
            {
                return new ErrorResult(Messages.Error);
            }

            return new SuccessResult(Messages.Success);
        }
    }
}