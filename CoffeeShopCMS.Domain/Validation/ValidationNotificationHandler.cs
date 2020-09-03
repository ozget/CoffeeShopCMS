using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopCMS.Domain.Validation
{
    public class ValidationNotificationHandler : IValidationNotificationHandler
    {
        private readonly List<string> notifications;

        public ValidationNotificationHandler()
        {
            notifications = new List<string>();
        }

        public List<string> GetNotifications()
        {
            return notifications;
        }

        public void HandleError(string notificationMessage)
        {
            notifications.Add(notificationMessage);
        }

        public bool HasErrors()
        {
            return notifications.Count > 0;
        }
    }
}