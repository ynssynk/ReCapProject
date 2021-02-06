using System;
using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class BrandManager:IBrandService
    {
        private IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        public void Add(Brand brand)
        {
            if (brand.Name.Length>2)
            {
               _brandDal.Add(brand);
            }
            else
            {
                throw new Exception("Olmadı !!! Az daha uzun değer girin ");
            }
        }
    }
}