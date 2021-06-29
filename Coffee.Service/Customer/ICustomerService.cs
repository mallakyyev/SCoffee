using System;
using System.Collections.Generic;
using System.Text;

namespace Coffee.Service.Customer
{
    public interface ICustomerService
    {
        public List<Data.Models.Customer> GetAllCustomers();
        public ServiceResponce<Data.Models.Customer> CreateCustomer(Data.Models.Customer customer);
        public ServiceResponce<bool> DeleteCustomer(int id);
        public Data.Models.Customer GetById(int id);


    }
}
