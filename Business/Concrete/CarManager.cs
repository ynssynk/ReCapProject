using System;
using System.Collections.Generic;
using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CarManager:ICarService
    {
        private ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        public List<Car> GetAll()
        {
            return _carDal.GetAll();
        }

        public Car Get()
        {
            throw new NotImplementedException();
        }


        public void Add(Car car)
        {
            if (car.DailyPrice>0)
            {
                _carDal.Add(car);
            }
            else
            {
                throw new NullReferenceException("Yanlış değer girdiniz");
            }
        }

        public void Update(Car car)
        {
            
            if (car != null)
            {
                _carDal.Update(car);
            }
            else
            {
                throw new Exception("Hata!!");
            }
        }

        public void Delete(Car car)
        {
            if (car!=null)
            {
                _carDal.Delete(car);
            }
            else
            {
                throw new Exception("Ters giden bir şeyler var !!! Tekrar kontrol et");
            }
        }

        public List<Car> GetCarsByBrandId(int brandId)
        {
            return _carDal.GetAll() == null ? throw new Exception("Listete hiç bu markadan araç yok ") : _carDal.GetAll(c => c.BrandId == brandId);
        }

        public List<Car> GetCarsByColorId(int colorId)
        {
            return _carDal.GetAll() == null
                ? throw new Exception("Bu renk ait hiç araç yok")
                : _carDal.GetAll(c => c.ColorId == colorId);
        }
    }
}