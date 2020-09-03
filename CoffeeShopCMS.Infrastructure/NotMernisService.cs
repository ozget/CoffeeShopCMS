using CoffeeShopCMS.Service.Provider;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeShopCMS.Infrastructure
{
    public class NotMernisService : IVerificationOperationsService
    {
        public bool IsCorrectTcFullNameBirtYear(long identityNo, string firstName, string lastName, int birthYear)
        {
            return true;
        }
    }
}