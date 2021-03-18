using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal:EfEntityRepositoryBase<Car,RecapContext>,ICarDal
    {
        public List<CarDetailDto> GetCarDetails(Expression<Func<Car, bool>> filter = null)
        {
            using (RecapContext context=new RecapContext())
            {
                var result = from car in filter is null ? context.Cars :context.Cars.Where(filter)
                    join b in context.Brands on car.BrandId equals b.Id
                    join color in context.Colors on car.ColorId equals color.Id
                    join carImage in context.CarImages on car.Id equals carImage.CarId into emptyValues
                    from carImage in emptyValues.DefaultIfEmpty()
                    select new CarDetailDto
                    {
                        Id = car.Id,
                        BrandId = b.Id,
                        ColorId = color.Id,
                        CarName = car.Name,
                        Description = car.Description,
                        DailyPrice = car.DailyPrice,
                        BrandName = b.Name,
                        ColorName = color.Name,
                        ImagePath = carImage.ImagePath
                    };
                return result.ToList();
            } 
        }
    }
}