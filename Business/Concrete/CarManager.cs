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

        public Car GetById(int id)
        {
            return id <= 0 ? throw new Exception("Geçersiz bir değer girildi") : _carDal.GetById(id);
        }

        public void Add(Car car)
        {
            if (car!=null)
            {
                _carDal.Add(car);
            }
            else
            {
                throw new NullReferenceException("Beklenilen değer girilmedi veya yanlış bir şey oldu");
            }
        }

        public void Update(Car car)
        {
            
            if (car != null && car.Id>0)
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
    }
}