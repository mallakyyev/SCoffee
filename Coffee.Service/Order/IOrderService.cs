using Coffee.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coffee.Service.Order
{
    public interface IOrderService
    {
        public List<SalesOrder> GetOrders();
        public ServiceResponce<bool> GenerateInvoiceForOrder(SalesOrder order);
        public ServiceResponce<bool> MArkFulfilled(int id);
    }
}
