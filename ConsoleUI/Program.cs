using System;
using System.Collections.Generic;
using System.Linq;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
           
            CarManager carManager = new CarManager(new EfCarDal());
            
            
            //GetAllList(carManager);
            Console.WriteLine("*******brandıd******");
            GetCarsBrandId(carManager);
            Console.WriteLine("****colorId*****");
            GetCarsColordId(carManager);

            

        }

        private static void GetAllList(CarManager carManager)
        {
            foreach (var car in carManager.GetAll())
            {
                Console.WriteLine("Id: {0} => Açıklama: {1} Günlük Fiyat: {2}", car.Id, car.Description, car.DailyPrice);
            }
        }

        private static void GetCarsBrandId(CarManager carManager)
        {
            foreach (var car in carManager.GetCarsByColorId(1))
            {
                Console.WriteLine("Id: {0} => Açıklama: {1} Günlük Fiyat: {2}", car.Id, car.Description, car.DailyPrice);
            }
        }
        private static void GetCarsColordId(CarManager carManager)
        {
            foreach (var car in carManager.GetCarsByColorId(2))
            {
                Console.WriteLine("Id: {0} => Açıklama: {1} Günlük Fiyat: {2}", car.Id, car.Description, car.DailyPrice);
            }
        }
    }
}
