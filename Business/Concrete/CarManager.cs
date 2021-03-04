using System;
using System.Collections.Generic;
using Business.Abstract;
using Business.BusinessAspects;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    [ValidationAspect(typeof(CarValidator))]
    public class CarManager : ICarService
    {
        private ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll());
        }

        public IDataResult<Car> Get(int carId)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.Id == carId));
        }


        public IResult Add(Car car)
        {
            if (car.DailyPrice<0)
            {
                return new ErrorResult(Messages.ArgumentNull);
            }
            _carDal.Add(car);
            return new SuccessResult(Messages.CarAdded);
        }

        public IResult Update(Car car)
        {

            if (car != null)
            {
                _carDal.Update(car);
                return new SuccessResult(Messages.CarUpdated);
            }

            return new ErrorResult(Messages.ArgumentNull);
        }

        public IResult Delete(Car car)
        {
            if (car != null)
            {
                _carDal.Delete(car);
                return new SuccessResult(Messages.CarDeleted);
            }

            return new ErrorResult(Messages.ArgumentNull);
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int brandId)
        {
            if (_carDal.GetAll()==null)
            {
                return new ErrorDataResult<List<Car>>(Messages.ArgumentNull);
            }

            return new SuccessDataResult<List<Car>>(_carDal.GetAll(b => b.BrandId == brandId));
        }

        public IDataResult<List<Car>> GetCarsByColorId(int colorId)
        {
            if (_carDal.GetAll()==null)
            {
                return new ErrorDataResult<List<Car>>(Messages.ArgumentNull);
            }

            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == colorId));
        }
        [SecuredOperation("car.getcardetail,admin")]
        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails());
        }

        public IDataResult<Car> GetById(int id)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.Id == id));
        }
    }
}