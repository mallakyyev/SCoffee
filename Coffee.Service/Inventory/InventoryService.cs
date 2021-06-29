using Coffee.Data;
using Coffee.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coffee.Service.Inventory
{
    public class InventoryService : IInventoryService
    {
        private readonly SolarDbContext _db;

        public InventoryService(SolarDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Create a snapshop for logging changes
        /// </summary>
        public void CreateSnapShot()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ProductInventory GetByProductId(int productId)
        {
            throw new NotImplementedException();
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

        public List<ProductInventorySnapshot> GetSnapShotHistory()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Updates number of units available of the product with id = id
        /// </summary>
        /// <param name="id">productId</param>
        /// <param name="adjustment">number of units added</param>
        /// <returns>ServiceResponce</returns>
        public ServiceResponce<ProductInventory> UpdateUnitsAvailable(int id, int adjustment)
        {
            var inventory = _db.ProductInventories
                .Include(inv => inv.Product)
                .First(inventory => inventory.Product.Id == id);
            inventory.QuantityOnHand += adjustment;
            _db.SaveChanges();
            return new ServiceResponce<ProductInventory>{
                IsSuccess = true,
                Data = inventory,
                Message = $"Product {id} inventory adjusted",
                Time = DateTime.UtcNow
            };
        }
    }
}
