using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoffeeShopCMS.Service.Services
{
    public interface IService<T>
    {
        IQueryable<T> GetAll();

        void Insert(T entity);
    }
}