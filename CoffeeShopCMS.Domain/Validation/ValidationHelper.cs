using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopCMS.Domain.Validation
{
    public class ValidationHelper
    {
        private const string nullError = "Değer boş olamaz";
        private const string countError = "Uzunluğu uygun değil";

        private IValidationNotificationHandler notificationHandler;

        public ValidationHelper(ref IValidationNotificationHandler notificationHandler)
        {
            this.notificationHandler = notificationHandler;
        }

        public void NullCheckStringValue(string name, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                notificationHandler.HandleError(string.Format("{0} {1}", name, nullError));
            }
        }

        public void NullAndCountCheckIntValue(string name, int? property, int? lengt)
        {
            if (property == null)
            {
                notificationHandler.HandleError(string.Format("{0} {1}", name, nullError));
            }
            if (property.ToString().Length != lengt)
            {
                notificationHandler.HandleError(string.Format("{0} {1}", name, countError));
            }
        }

        public void NullAndCountCheckLongValue(string name, long? property, int? lengt)
        {
            if (property == null)
            {
                notificationHandler.HandleError(string.Format("{0} {1}", name, nullError));
            }
            if (property.ToString().Length != lengt)
            {
                notificationHandler.HandleError(string.Format("{0} {1}", name, countError));
            }
        }
    }
}