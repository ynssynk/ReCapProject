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
    public class EfCustomerDal:EfEntityRepositoryBase<Customer,RecapContext>,ICustomerDal
    {
        public List<CustomerDetailDto> GetCustomerDetails(Expression<Func<Customer, bool>> filter = null)
        {
            using (RecapContext context= new RecapContext())
            {
                var result = from customer in filter is null ? context.Customers : context.Customers.Where(filter)
                    join user in context.Users on customer.UserId equals user.Id 
                    select new CustomerDetailDto
                    {
                        CompanyName = customer.CompanyName,
                        FirstName = user.FirstName,
                        LastName = user.LastName
                    };
                return result.ToList();
            }
        }
    }
}