﻿using Coffee.Service.Customer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SCoffee.Serialization;
using SCoffee.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCoffee.Controllers
{
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;

        public CustomerController(ILogger<CustomerController> loggger, ICustomerService customerService)
        {
            _logger = loggger;
            _customerService = customerService;
        }

        [HttpPost("/api/customer")]
        public ActionResult CreateCustomer([FromBody] CustomerModel customer)
        {
            _logger.LogInformation("Creating a new customer");
            customer.CreatedOn = DateTime.UtcNow;
            customer.UpdatedOn = DateTime.UtcNow;
            var customerData = CustomerMapper.SerializeCustomer(customer);
            var newCustomer = _customerService.CreateCustomer(customerData);
            return Ok(newCustomer);
        }  
        
        [HttpGet("/api/customer")]
        public ActionResult GetCustomers()
        {
            _logger.LogInformation("Getting all customers");
            var customers = _customerService.GetAllCustomers();

            var customerModels = customers.Select(customer => new CustomerModel
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PrimaryAddress = CustomerMapper.MapCustomerAddress(customer.PrimaryAddress),
                CreatedOn = customer.CreatedOn,
                UpdatedOn = customer.UpdatedOn
            }).OrderByDescending(customer => customer.CreatedOn).ToList() ;

            return Ok(customerModels);
        }

        [HttpDelete("/api/customer/{id}")]
        public ActionResult DeleteCustomer(int id)
        {
            _logger.LogInformation($"Deleting cutomer {id}");
            var responce = _customerService.DeleteCustomer(id);
            return Ok(responce);
        }
    }
}