using System;
using System.Collections.Generic;
using System.Linq;
using Business.Concrete;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Car> cars = new List<Car>()
            {
                new Car
                {
                    Id = 1, BrandId = 1, ColorId = 1, DailyPrice = 250,ModelYear = 2018,
                    Description = "RENAULT CLIO AT 1.5 -Ekonomik araç"
                },
                new Car
                {
                    Id = 2, BrandId = 2, ColorId = 1, DailyPrice = 300,ModelYear = 2019,
                    Description = "FORD FOCUS AT 1.5 -Orta sınıf araç"
                },
                new Car
                {
                    Id = 3, BrandId = 3, ColorId = 1, DailyPrice = 300,ModelYear = 2019,
                    Description = "TOYOTA COROLLA HYBRID -Orta sınıf araç"
                },
                new Car {Id = 4, BrandId = 3, ColorId = 1, DailyPrice = 350,ModelYear = 2020, Description = "BMW 320i -Üst snıf araç"},
                new Car {Id = 5, BrandId = 4, ColorId = 1, DailyPrice = 370, Description = "VOLVO S90 -Üst sınıf araç"},
                new Car
                {
                    Id = 6, BrandId = 3, ColorId = 1, DailyPrice = 385,ModelYear = 2021, Description = "BMW 2 serisi -Üst snıf araç"
                },
            };
            CarManager carManager = new CarManager(new InMemoryDal());
            
            Console.WriteLine("********Listele**********");
            carManager.GetAll(cars);
            
            Car car1 = new Car { Id = 7, BrandId = 1, ColorId = 2, DailyPrice = 200, ModelYear = 2020, Description = "FIAT EGEA 1.3" };
            
            Console.WriteLine("**********Yeni kayıt ekle*********");
            carManager.Add(car1);
            
            Console.WriteLine("*********Listele*********");
            carManager.GetAll(cars);

            Car car2 =  new Car { Id = 1, BrandId = 1, ColorId = 2, DailyPrice = 200, ModelYear = 2020, Description = "Opel Insignia" };

            Console.WriteLine("**********Güncelle*********");
            carManager.Update(car2);
            
            Console.WriteLine("********Listele*********");
            carManager.GetAll(cars);
            
            Console.WriteLine("**********Araç Sil*********");
            carManager.Delete(car2);
            
            Console.WriteLine("*********Listele********");
            carManager.GetAll(cars);
            
            Console.WriteLine("*********Tek bir kayıt göster*********");
            carManager.GetById(2);

        }

        
    }
}
