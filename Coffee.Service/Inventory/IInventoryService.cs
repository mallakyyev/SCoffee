using System;
using System.Collections.Generic;
using System.Text;

namespace Coffee.Service.Inventory
{
    public interface IInventoryService
    {
        public List<Data.Models.ProductInventory> GetCurrentInventory();
        public ServiceResponce<Data.Models.ProductInventory> UpdateUnitsAvailable(int id, int adjustment);
        public Data.Models.ProductInventory GetByProductId(int productId);
       
        public List<Data.Models.ProductInventorySnapshot> GetSnapShotHistory();
    }
}
