using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfBrandDal:IBrandDal
    {
        public List<Brand> GetAll(Expression<Func<Brand, bool>> filter = null)
        {
            using (RecapContext context = new RecapContext())
            {
                return filter != null ? context.Set<Brand>().Where(filter).ToList() : context.Set<Brand>().ToList();
            }
        }

        public Brand Get(Expression<Func<Brand, bool>> filter)
        {
            using (RecapContext context = new RecapContext())
            {
                return context.Set<Brand>().SingleOrDefault(filter);
            }
        }

        public void Add(Brand entity)
        {
            using (RecapContext context=new RecapContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Update(Brand entity)
        {
            using (RecapContext context = new RecapContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Delete(Brand entity)
        {
            using (RecapContext context = new RecapContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }
}