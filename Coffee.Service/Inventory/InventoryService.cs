using Coffee.Data;
using Coffee.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coffee.Service.Inventory
{
    public class InventoryService : IInventoryService
    {
        private readonly SolarDbContext _db;
        private readonly ILogger<InventoryService> _logger;
        public InventoryService(SolarDbContext db, ILogger<InventoryService> logger)
        {
            _db = db;
            _logger = logger;
        }

        /// <summary>
        /// Create a snapshop for logging changes
        /// </summary>
        private  void CreateSnapShot(ProductInventory pi)
        {
            var now = DateTime.UtcNow;
            var snapshot = new ProductInventorySnapshot
            {
                SnapshotTime = now,
                Product =pi.Product,
                QuantityOnHand = pi.QuantityOnHand
            };
            _db.Add(snapshot);
            
        }

        /// <summary>
        /// Gets a ProductInventory instance by product id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ProductInventory GetByProductId(int productId)
        {
            return _db.ProductInventories
                .Include(pi => pi.Product)
                .FirstOrDefault(pi => pi.Product.Id == productId);
        }

        /// <summary>
        /// Returns all current inventory from the database
        /// </summary>
        /// <returns></returns>
        public List<ProductInventory> GetCurrentInventory()
        {
            return _db.ProductInventories
                .Include(pi => pi.Product)
                .Where(pi => !pi.Product.IsArchived).ToList();
        }


        /// <summary>
        /// Gets the history of the snapshot for the previous x hours
        /// </summary>
        /// <returns></returns>
        public List<ProductInventorySnapshot> GetSnapShotHistory()
        {
            var earliest = DateTime.UtcNow - TimeSpan.FromHours(6);
            return _db.ProductInventorySnapshots
                .Include(nap => nap.Product)
                .Where(nap => nap.SnapshotTime > earliest 
                            && !nap.Product.IsArchived)
                .ToList();
        }


        /// <summary>
        /// Updates number of units available of the product with id = id
        /// </summary>
        /// <param name="id">productId</param>
        /// <param name="adjustment">number of units added</param>
        /// <returns>ServiceResponce</returns>
        public ServiceResponce<ProductInventory> UpdateUnitsAvailable(int id, int adjustment)
        {
            try
            {
                var inventory = _db.ProductInventories
                    .Include(inv => inv.Product)
                    .First(inventory => inventory.Product.Id == id);
                inventory.QuantityOnHand += adjustment;
                try
                {
                    CreateSnapShot(inventory);
                }catch(Exception e)
                {
                    _logger.LogError("");
                }
                _db.SaveChanges();
                return new ServiceResponce<ProductInventory>
                {
                    IsSuccess = true,
                    Data = inventory,
                    Message = $"Product {id} inventory adjusted",
                    Time = DateTime.UtcNow
                };
            }catch(Exception e)
            {
                return new ServiceResponce<ProductInventory>
                {
                    IsSuccess = false,
                    Data = null,
                    Message = e.Message,
                    Time = DateTime.UtcNow
                };
            }
        }
    }
}
