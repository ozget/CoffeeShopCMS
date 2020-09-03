using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CoffeeShopCMS.Domain.Repositories
{
    public interface IRepository<T> where T : class
    {
        void Insert(T entity);

        T Find(int id);

        T GetSingle(Expression<Func<T, bool>> expression, string[] includeProperties = null);

        IQueryable<T> GetAll(Expression<Func<T, bool>> whereCondition, Expression<Func<T, object>> orderCondition = null, bool desc = false, int? skip = null, int? take = null, string[] includeProperties = null);
    }
}