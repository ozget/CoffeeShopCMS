using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopCMS.Domain.Validation
{
    public abstract class Validator
    {
        public IValidationNotificationHandler notificationHandler = new ValidationNotificationHandler();

        public virtual IValidationNotificationHandler GetValidationNotificationHandler()
        {
            return this.notificationHandler;
        }

        public abstract void Validate();
    }
}