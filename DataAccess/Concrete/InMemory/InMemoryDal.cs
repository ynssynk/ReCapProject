using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryDal:ICarDal
    {
        private List<Car> _cars;

        public InMemoryDal()
        {
            _cars = new List<Car>
            {
                new Car
                {
                    Id = 1,
                    BrandId = 1,
                    ColorId = 1,
                    DailyPrice = 250,
                    ModelYear = 2018,
                    Description = "RENAULT CLIO AT 1.5 -Ekonomik araç"
                },
                new Car
                {
                    Id = 2,
                    BrandId = 2,
                    ColorId = 1,
                    DailyPrice = 300,
                    ModelYear = 2019,
                    Description = "FORD FOCUS AT 1.5 -Orta sınıf araç"
                },
                new Car
                {
                    Id = 3,
                    BrandId = 3,
                    ColorId = 1,
                    DailyPrice = 300,
                    ModelYear = 2019,
                    Description = "TOYOTA COROLLA HYBRID -Orta sınıf araç"
                },
                new Car
                {
                    Id = 4, BrandId = 3, ColorId = 1, DailyPrice = 350, ModelYear = 2020,
                    Description = "BMW 320i -Üst snıf araç"
                },
                new Car {Id = 5, BrandId = 4, ColorId = 1, DailyPrice = 370, Description = "VOLVO S90 -Üst sınıf araç"},
                new Car
                {
                    Id = 6,
                    BrandId = 3,
                    ColorId = 1,
                    DailyPrice = 385,
                    ModelYear = 2021,
                    Description = "BMW 2 serisi -Üst snıf araç"
                },
            };
        }

        public List<Car> GetAll()
        {
            return _cars;
        }

        public Car GetById(int id)
        {
            return _cars.FirstOrDefault(c => c.Id == id);
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Update(Car car)
        {
            var cartoUpdate = _cars.SingleOrDefault(c => c.Id == car.Id);
            cartoUpdate.BrandId = car.BrandId;
            cartoUpdate.ColorId = car.ColorId;
            cartoUpdate.DailyPrice = car.DailyPrice;
            cartoUpdate.ModelYear = car.ModelYear;
            cartoUpdate.Description = car.Description;
        }
        public void Delete(Car car)
        {
            var carToDelete = _cars.SingleOrDefault(c => c.Id == car.Id);
            _cars.Remove(carToDelete);
        }

        public List<CarDetailDto> GetCarDetails()
        {
            throw new NotImplementedException();
        }
    }
}