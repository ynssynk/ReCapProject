using System;
using System.Collections.Generic;
using System.Linq;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Car car1 = new Car
            {
                BrandId = 1,ColorId = 3,DailyPrice = 150,Description = "En uygun fiyat araçlar bizde",ModelYear = 2020
            };
            //Brand brand1 = new Brand {Id = 1002,Name = "Fiat"};
            Color color1 = new Color {Name = "Mavi"};
            CarManager carManager = new CarManager(new EfCarDal());
            BrandManager brandManager = new BrandManager(new EfBrandDal());
            //brandManager.Update(brand1);
            ColorManager colorManager = new ColorManager(new EfColorDal());
            colorManager.Add(color1);
            GetAllList(carManager);
            Console.WriteLine("*****Markalar****");
            GetAllBrands(brandManager);
            Console.WriteLine("****Renkler*****");
            GetAllColors(colorManager);
            

        }

        private static void GetAllColors(ColorManager colorManager)
        {
            foreach (var color in colorManager.GetAll())
            {
                
                Console.WriteLine("Id: {0} / Renk: {1}",color.Id,color.Name);
            }
        }
        private static void GetAllBrands(BrandManager brandManager)
        {
            foreach (var brand in brandManager.GetAll())
            {
                Console.WriteLine("Id: {0} / Marka: {1}",brand.Id,brand.Name);
            }
        }
        private static void GetAllList(CarManager carManager)
        {
            foreach (var car in carManager.GetCarDetails())
            {
                Console.WriteLine("Marka: {0}  Renk:{1}  Günlük Fiyat: {2} Açıklama:  {3}",car.BrandName,car.ColorName,car.DailyPrice,car.CarName);
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
