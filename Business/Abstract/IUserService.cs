using System.Collections.Generic;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<User>> GetAll();
        IDataResult<User> Get(int userId);
        IResult Add(User user);
        IResult Update(User user);
        IResult Delete(User user);
    }
}