using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryDal:ICarDal
    {
        private List<Car> _cars;


        public List<Car> GetAll(List<Car> cars)
        {
            _cars = cars;
            foreach (var car in _cars)
            {
                Console.WriteLine("Id:{0} => {1} --- Günlük Fiyat: {2}",car.Id,car.Description,car.DailyPrice);
            }

            return _cars;
        }

        public Car GetById(int id)
        {
            var result = _cars.Find(c => c.Id == id);
            Console.WriteLine("Id:{0}=>{1} -*-*- Günlük Fiyat: {2}",result.Id,result.Description,result.DailyPrice);
            return result;
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
    }
}