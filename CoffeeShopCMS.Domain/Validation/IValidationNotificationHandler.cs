using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopCMS.Domain.Validation
{
    public interface IValidationNotificationHandler
    {
        void HandleError(string notificationMessage);

        bool HasErrors();

        List<string> GetNotifications();
    }
}