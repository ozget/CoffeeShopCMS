using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoffeeShopCMS.Service.Services
{
    public class Service<T> : IService<T> where T : class
    {
        public IQueryable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        //private readonly IRepository<T> repository;

        //protected Service(IRepository<T> repository)
        //{
        //    this.repository = repository;
        //}

        public void Insert(T entity)
        {
            // repository.Insert(entity);
        }
    }
}