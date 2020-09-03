using CoffeeShopCMS.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeShopCMS.Domain.Entities
{
    public abstract class BaseEntity : Validator
    {
        public DateTime? CreateDate { get; set; }
    }
}