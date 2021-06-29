using Coffee.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coffee.Service.Order
{
    public interface IOrderService
    {
        public List<SalesOrder> GetOrders();
        public ServiceResponce<bool> GenerateOpenOrder(SalesOrder order);
        public ServiceResponce<bool> MarkFulfilled(int id);
    }
}
