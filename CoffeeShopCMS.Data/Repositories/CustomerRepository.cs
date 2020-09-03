using CoffeeShopCMS.Domain.Entities;
using CoffeeShopCMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeShopCMS.Data.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}