using AutoMapper;
using CoffeeShopCMS.Domain.Entities;
using CoffeeShopCMS.Domain.Repositories;
using CoffeeShopCMS.Service.Dto;
using CoffeeShopCMS.Service.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoffeeShopCMS.Service.Services
{
    public class CustomerService : Service<Customer>, ICustomerService
    {
        private readonly IMapper mapper;
        private readonly ICustomerRepository customerRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IVerificationOperationsService verificationOperationsService;

        public CustomerService(IMapper mapper, ICustomerRepository customerRepository, IUnitOfWork unitOfWork, IVerificationOperationsService verificationOperationsService)
        {
            this.mapper = mapper;
            this.customerRepository = customerRepository;
            this.unitOfWork = unitOfWork;
            this.verificationOperationsService = verificationOperationsService;
        }

        public int CreateCustomer(CustomerDto customerDto)
        {
            if (!IsCustomerFullNameAvailable(customerDto.FirstName, customerDto.LastName))
            {
                throw new ArgumentException($"{string.Concat(customerDto.FirstName, " ", customerDto.LastName)}: Aynı isimli  kayıt bulunuyor.");
            }
            if (!IsCustomerTcAvailable(customerDto.Tc))
            {
                throw new InvalidOperationException("Kayıt  daha önce eklenmiş");
            }
            var customer = mapper.Map<Customer>(customerDto);
            customer.Validate();

            var handler = customer.GetValidationNotificationHandler();
            if (handler.HasErrors())
            {
                throw new InvalidOperationException(string.Join(",", handler.GetNotifications()));
            }

            if (!verificationOperationsService.IsCorrectTcFullNameBirtYear(customerDto.Tc, customerDto.FirstName, customerDto.LastName, customerDto.BirthYear))
            {
                throw new InvalidOperationException("Kimlik numarası ve Adı uyuşmuyor");
            }
           
            customerRepository.Insert(customer);
            unitOfWork.Commit();

            return customer.Id;
        }

        private bool IsCustomerFullNameAvailable(string firstName, string lastName)
        {
            var exists = customerRepository.GetAll(p => p.FirstName == firstName && p.LastName == lastName).Any();
            if (exists)
            {
                return false;
            }
            return true;
        }

        private bool IsCustomerTcAvailable(long tc)
        {
            var exists = customerRepository.GetAll(p => p.Tc == tc).Any();
            if (exists)
            {
                return false;
            }
            return true;
        }
    }
}