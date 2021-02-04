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
           
            CarManager carManager = new CarManager(new InMemoryDal());
            
            Console.WriteLine("********Listele**********");
            GetAllList(carManager);

            Car car1 = new Car { Id = 7, BrandId = 1, ColorId = 2, DailyPrice = 200, ModelYear = 2020, Description = "FIAT EGEA 1.3" };

            Console.WriteLine("**********Yeni kayıt ekle*********");
            carManager.Add(car1);

            Console.WriteLine("*********Listele*********");
            GetAllList(carManager);

            Car car2 = new Car { Id = 1, BrandId = 1, ColorId = 2, DailyPrice = 200, ModelYear = 2020, Description = "Opel Insignia" };

            Console.WriteLine("**********Güncelle*********");
            carManager.Update(car2);

            Console.WriteLine("********Listele*********");
            GetAllList(carManager);

            Console.WriteLine("**********Araç Sil*********");
            carManager.Delete(car2);

            Console.WriteLine("*********Listele********");
            GetAllList(carManager);

            Console.WriteLine("*********Tek bir kayıt göster*********");
            Console.WriteLine(carManager.GetById(2).Description);

        }

        private static void GetAllList(CarManager carManager)
        {
            foreach (var car in carManager.GetAll())
            {
                Console.WriteLine("Id: {0} => Açıklama: {1} Günlük Fiyat: {2}", car.Id, car.Description, car.DailyPrice);
            }
        }
    }
}
