using System;
using System.Collections.Generic;
using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class ColorManager:IColorService
    {
        private IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        public List<Color> GetAll()
        {
            return _colorDal.GetAll();
        }

        public void Add(Color color)
        {
            if (color.Name.Length>=2)
            {
                _colorDal.Add(color);
            }
            else
            {
                throw new Exception("Yeterli değer girilmedi");
            }
        }
    }
}