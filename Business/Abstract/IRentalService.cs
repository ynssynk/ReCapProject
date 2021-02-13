using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IRentalService
    {
        IDataResult<List<Rental>> GetAll(Expression<Func<Rental, bool>> filter = null);
        IDataResult<List<RentalDetailDto>> GetAllRent(); 
        IDataResult<Rental> Get(int id);
        IResult Add(Rental rental);
        IResult Update(Rental rental);
        IResult Delete(Rental rental);
    }
}