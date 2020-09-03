using System;
using System.Collections.Generic;
using System.Text;
using CoffeeShopCMS.Domain.Entities;
using CoffeeShopCMS.Service.Dto;

namespace CoffeeShopCMS.Service.Services
{
    public interface ICustomerService : IService<Customer>
    {
        int CreateCustomer(CustomerDto customerDto);
    }
}