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
        public List<CarDetailDto> GetCarDetails()
        {
            using (RecapContext context=new RecapContext())
            {
                var result = from c in context.Cars
                    join b in context.Brands on c.BrandId equals b.Id
                    join color in context.Colors on c.ColorId equals color.Id
                    select new CarDetailDto
                    {
                        CarName = c.Name,Description = c.Description,DailyPrice = c.DailyPrice,BrandName = b.Name,ColorName = color.Name
                    };
                return result.ToList();
            } 
        }
    }
}