using CoffeeShopCMS.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CoffeeShopCMS.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        internal ApplicationContext dbContext;
        internal DbSet<T> dbSet;

        public Repository(ApplicationContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<T>();
        }

        public T Find(int id)
        {
            return dbSet.Find(id);
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> whereCondition, Expression<Func<T, object>> orderCondition = null, bool desc = false, int? skip = null, int? take = null, string[] includeProperties = null)
        {
            IQueryable<T> data = dbSet;
            if (includeProperties != null && includeProperties.Length > 0)
            {
                data = dbSet.Include(includeProperties[0]);
                for (int i = 1; i < includeProperties.Length; i++)
                {
                    data = data.Include(includeProperties[i]);
                }
            }

            data = data.Where(whereCondition);

            if (orderCondition != null)
            {
                if (desc)
                {
                    data = data.OrderByDescending(orderCondition);
                }
                else
                {
                    data = data.OrderBy(orderCondition);
                }
            }

            if (skip.HasValue)
            {
                data = data.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                data = data.Take(take.Value);
            }

            return data;
        }

        public T GetSingle(Expression<Func<T, bool>> expression, string[] includeProperties = null)
        {
            IQueryable<T> data = dbSet;
            if (includeProperties != null && includeProperties.Length > 0)
            {
                for (int i = 0; i < includeProperties.Length; i++)
                {
                    data = data.Include(includeProperties[i]);
                }
            }
            return data.Where(expression).FirstOrDefault();
        }

        public void Insert(T entity)
        {
            dbSet.Attach(entity);
        }
    }
}