using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;

namespace CoffeeShopCMS.Service.Provider
{
    public interface IVerificationOperationsService
    {
        bool IsCorrectTcFullNameBirtYear(long identityNo, string firstName, string lastName, int birthYear);
    }
}