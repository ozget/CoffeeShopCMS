using CoffeeShopCMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace CoffeeShopCMS.Data
{
    public class ApplicationContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
    }
}