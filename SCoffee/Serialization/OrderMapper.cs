using Coffee.Data.Models;
using SCoffee.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCoffee.Serialization
{
    /// <summary>
    /// Handles Mapping Oder data Models to related View Models
    /// </summary>
    public static class OrderMapper
    {
        /// <summary>
        /// Maps InvoiceModel view model to a SalesOrdee data model
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        public static SalesOrder SerializeInvoiceOrder(InvoiceModel invoice)
        {
            var salesOrderItem = invoice.LineItems
                .Select(item => new SalesOrderItem
                {
                    Id = item.Id,
                    Quantity = item.Quantity,
                    Product = ProductMapper.SerialiazeProductModel(item.Product)
                }).ToList();
            return new SalesOrder
            {
                SalesOrderItems = salesOrderItem,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
            };
        }

        public static List<OrderModel> SerializingOrdersToViewModels(IEnumerable<SalesOrder> orders)
        {
            return orders.Select(order => new OrderModel
            {
                Id = order.Id,
                CreatedOn = order.CreatedOn,
                UpdatedOn = order.UpdatedOn,
                SalesOrderItems = SerializeSalesOrderItems(order.SalesOrderItems),
                Customer = CustomerMapper.SerializeCustomer(order.Customer),
                IsPaid = order.IsPaid

            }).ToList();
        }
        
        private static List<SalesOrderItemModel> SerializeSalesOrderItems(IEnumerable<SalesOrderItem> orderItems)
        {
            return orderItems.Select(item => new SalesOrderItemModel
            {
                Id = item.Id,
                Quantity = item.Quantity,
                Product = ProductMapper.SerialiazeProductModel(item.Product),
            }).ToList();
        }


    }
}
