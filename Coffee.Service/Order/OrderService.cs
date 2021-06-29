using Coffee.Data;
using Coffee.Data.Models;
using Coffee.Service.Inventory;
using Coffee.Service.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coffee.Service.Order
{
    public class OrderService : IOrderService
    {
        private readonly SolarDbContext _db;
        private readonly ILogger<OrderService> _logger;
        private readonly IProductService _productService;
        private readonly IInventoryService _inventoryService;
        public OrderService(SolarDbContext db, ILogger<OrderService> logger, IProductService productService, IInventoryService inventoryService)
        {
            _logger = logger;
            _db = db;
            _productService = productService;
            _inventoryService = inventoryService;
        }

        /// <summary>
        /// Creates an open SalesOrder
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public ServiceResponce<bool> GenerateOpenOrder(SalesOrder order)
        {
            foreach(var item in order.SalesOrderItems)
            {
                item.Product = _productService.GetProductById(item.Product.Id);
                
                var inventoryId = _inventoryService.GetByProductId(item.Product.Id).Id;
                _inventoryService.UpdateUnitsAvailable(inventoryId, -item.Quantity);
            }
            try
            {
                _db.SalesOrders.Add(order);
                _db.SaveChanges();
                return new ServiceResponce<bool>
                {
                    IsSuccess = true,
                    Data = true,
                    Message = "Open Order Created",
                    Time = DateTime.UtcNow
                };
            }catch(Exception e)
            {
                return new ServiceResponce<bool>
                {
                    IsSuccess = false,
                    Data = false,
                    Message = e.Message,
                    Time = DateTime.UtcNow
                };
            }
        }


        /// <summary>
        /// Gets all SalesOrders from the system
        /// </summary>
        /// <returns></returns>
        public List<SalesOrder> GetOrders()
        {
            return _db.SalesOrders
                 .Include(so => so.Customer)
                    .ThenInclude(customer => customer.PrimaryAddress)
                 .Include(so => so.SalesOrderItems)
                    .ThenInclude(item => item.Product)
                 .ToList();
        }

        /// <summary>
        /// Marks an open SalesOrder as paid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ServiceResponce<bool> MarkFulfilled(int id)
        {
            var now = DateTime.UtcNow;
            var order = _db.SalesOrders.Find(id);
            order.UpdatedOn = now;
            order.IsPaid = true;
            try
            {
                _db.SalesOrders.Update(order);
                _db.SaveChanges();

                return new ServiceResponce<bool>
                {
                    IsSuccess = true,
                    Data = true,
                    Message = "SalesOrder Marked as paid",
                    Time = now
                };
            }catch(Exception e)
            {
                return new ServiceResponce<bool>
                {
                    IsSuccess = false,
                    Data = false,
                    Message = e.Message,
                    Time = now
                };
            }
        }
    }
}
