using CoffeeShopCMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeShopCMS.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext dbContext;

        public UnitOfWork(ApplicationContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Commit()
        {
            dbContext.SaveChanges();
        }
    }
}