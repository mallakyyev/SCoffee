using Coffee.Service.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SCoffee.Serialization;
using SCoffee.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCoffee.Controllers
{
    [ApiController]
    public class InventoryController : ControllerBase
    {

        private readonly IInventoryService _inventory;
        private ILogger<InventoryController> _logger;

        public InventoryController(ILogger<InventoryController> logger, IInventoryService inventoryService)
        {
            _inventory = inventoryService;
            _logger = logger;
        }

        [HttpGet("api/inventory")]
        public ActionResult GetCurrentInventory()
        {
            _logger.LogInformation("Getting all inventories");
            var inventoryies = _inventory.GetCurrentInventory()
                .Select(pi => new ProductInventoryModel
                {
                    Id = pi.Id,
                    Product = ProductMapper.SerialiazeProductModel(pi.Product),
                    IdealQuantity = pi.IdealQuantity,
                    QuantityOnHand = pi.QuantityOnHand
                })
                .OrderBy(env => env.Product.Name)
                .ToList();

            return Ok(inventoryies);
        }


        [HttpPatch("api/inventory")]
        public ActionResult UpdateInventory([FromBody] ShippmentModel shipment)
        {
            _logger.LogInformation($"Updating inventory for {shipment.ProductId} Adjustment {shipment.Adjustment}");
            var id = shipment.ProductId;
            var adjustment = shipment.Adjustment;
            var inventory = _inventory.UpdateUnitsAvailable(id, adjustment);
            return Ok();
        } 
    }
}
