using Coffee.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coffee.Service.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly SolarDbContext _db;
        public CustomerService(SolarDbContext db)
        {
            _db = db;
        } 

        /// <summary>
        /// Adds new customer record to the database
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>ServiceResponce</returns>
        public ServiceResponce<Data.Models.Customer> CreateCustomer(Data.Models.Customer customer)
        {
            try
            {
                _db.Customers.Add(customer);
                _db.SaveChanges();
                return new ServiceResponce<Data.Models.Customer>
                {
                    IsSuccess = true,
                    Data = customer,
                    Message = "Customer created successfully",
                    Time = DateTime.UtcNow
                };
            }catch(Exception e)
            {
             return new ServiceResponce<Data.Models.Customer>
                {
                    IsSuccess = false,
                    Data = customer,
                    Message = e.Message,
                    Time = DateTime.UtcNow
                };
}
        }


        /// <summary>
        /// Deletes the customer record from the databse if exists 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ServiceResponce</returns>
        public ServiceResponce<bool> DeleteCustomer(int id)
        {
            var customer = _db.Customers.Find(id);
            if(customer == null)
            {
                return new ServiceResponce<bool>
                {
                    IsSuccess = false,
                    Data = false,
                    Time = DateTime.UtcNow,
                    Message = "Customer to delete not found"
                };
            }else
            {
                try
                {
                    _db.Customers.Remove(customer);
                    _db.SaveChanges();
                    return new ServiceResponce<bool>
                    {
                        IsSuccess = true,
                        Data = true,
                        Time = DateTime.UtcNow,
                        Message = "Customer deleted successfully"
                    };
                }
                catch(Exception e)
                {
                    return new ServiceResponce<bool>
                    {
                        IsSuccess = false,
                        Data = false,
                        Time = DateTime.UtcNow,
                        Message = e.Message
                    };
                }
                
            }
        }

        /// <summary>
        /// Returns the list of customers from the database
        /// </summary>
        /// <returns>List<Customer></returns>
        public List<Data.Models.Customer> GetAllCustomers()
        {
            return _db.Customers
                .Include(customer => customer.PrimaryAddress)
                .OrderBy(customer => customer.LastName).ToList();
        }

        /// <summary>
        /// Gets a Customer recors for the database by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Data.Models.Customer GetById(int id)
        {
            return _db.Customers.Find(id);
        }
    }
}
