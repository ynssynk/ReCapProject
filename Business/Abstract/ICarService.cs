using System.Collections.Generic;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICarService
    {
        List<Car> GetAll();
        Car Get();
        void Add(Car car);
        void Update(Car car);
        void Delete(Car car);
        List<Car> GetCarsByBrandId(int brandId);
        List<Car> GetCarsByColorId(int colorId);

    }
}