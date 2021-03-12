using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal:EfEntityRepositoryBase<Rental,RecapContext>,IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetails(Expression<Func<Rental, bool>> filter = null)
        {
            using (RecapContext context=new RecapContext())
            {
                var result = from rental in filter is null ? context.Rentals : context.Rentals.Where(filter) 
                    join car in context.Cars on rental.Id equals car.Id
                    join customer in context.Customers on rental.CustomerId equals customer.Id
                    join user in context.Users on customer.UserId equals user.Id
                    join brand in context.Brands on car.BrandId equals brand.Id
                    select new RentalDetailDto
                    {
                        RentalId = rental.Id,
                        CarName = brand.Name,
                        DailyPrice = car.DailyPrice,
                        CompanyName = customer.CompanyName,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        RentDate = rental.RentDate,
                        ReturnDate = rental.ReturnDate

                    };
                return result.ToList();

            }
        }
    }
}