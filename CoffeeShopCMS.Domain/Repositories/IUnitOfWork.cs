using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeShopCMS.Domain.Repositories
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}