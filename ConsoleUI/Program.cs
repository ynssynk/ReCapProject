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
           
            CarManager carManager = new CarManager(new EfCarDal());

            GetAllList(carManager);

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
            
            var result = carManager.GetCarDetails();
            if (result.Success)
            {
                foreach (var car in carManager.GetCarDetails().Data)
                {
                    Console.WriteLine("Marka: {0}  Renk:{1}  Günlük Fiyat: {2} Açıklama:  {3}",car.BrandName,car.ColorName,car.DailyPrice,car.CarName);
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }
        }

        private static void GetCarsBrandId(CarManager carManager)
        {

            var result = carManager.GetCarsByColorId(1);
            if (result.Success)
            {
                foreach (var car in result.Data)
                {
                    Console.WriteLine("Id: {0} => Açıklama: {1} Günlük Fiyat: {2}", car.Id, car.Description, car.DailyPrice);
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }
            
        }
        private static void GetCarsColordId(CarManager carManager)
        {
            var result = carManager.GetCarsByColorId(2);
            if (result.Success)
            {
                foreach (var car in result.Data)
                {
                    Console.WriteLine("Id: {0} => Açıklama: {1} Günlük Fiyat: {2}", car.Id, car.Description, car.DailyPrice);
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }
        }
    }
}
