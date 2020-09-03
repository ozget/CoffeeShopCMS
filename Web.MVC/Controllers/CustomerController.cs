using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShopCMS.Service.Dto;
using CoffeeShopCMS.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.MVC.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService customerService;

        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("FirstName", "LastName", "Tc", "BirthYear")]CustomerDto customerDto)
        {
            try
            {
                customerService.CreateCustomer(customerDto);
                ViewBag.UserExists = "Kayıt oluşturuldu";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.UserExists = ex.Message;
                //  return BadRequest(ex.Message);
                return View();
            }
        }
    }
}