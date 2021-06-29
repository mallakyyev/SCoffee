using Coffee.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coffee.Service.Order
{
    public class OrderService : IOrderService
    {
        public ServiceResponce<bool> GenerateInvoiceForOrder(SalesOrder order)
        {
            throw new NotImplementedException();
        }

        public List<SalesOrder> GetOrders()
        {
            throw new NotImplementedException();
        }

        public ServiceResponce<bool> MArkFulfilled(int id)
        {
            throw new NotImplementedException();
        }
    }
}
