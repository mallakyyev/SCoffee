using Coffee.Data;
using Coffee.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coffee.Service.Product
{
    public class ProductService : IProductService
    {
        private readonly SolarDbContext _db;
        public ProductService(SolarDbContext db)
        {
            _db = db;
        }
        /// <summary>
        /// Archives a Product with product id equal to id by setting IsArchive=true;
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Service Responce</returns>
        public ServiceResponce<Data.Models.Product> ArchiveProduct(int id)
        {
            try
            {
                Data.Models.Product p = _db.Products.Find(id);
                if (p != null)
                {
                    p.IsArchived = true;
                    _db.Products.Update(p);
                    _db.SaveChanges();
                    return new ServiceResponce<Data.Models.Product>
                    {
                        Data = p,
                        IsSuccess = true,
                        Message = "Product archived successfully",
                        Time = DateTime.UtcNow
                    };
                }
                else
                {
                    return new ServiceResponce<Data.Models.Product>
                    {
                        Data = p,
                        IsSuccess = false,
                        Message = "Product with specified id could not be found",
                        Time = DateTime.UtcNow
                    };
                }
            }catch(Exception e)
            {
                return new ServiceResponce<Data.Models.Product>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = e.Message,
                    Time = DateTime.UtcNow
                };
            }
        }

        /// <summary>
        /// Creates new product and adds it to the database
        /// </summary>
        /// <param name="product"></param>
        /// <returns>Service Responce Model</returns>
        public ServiceResponce<Data.Models.Product> CreateProduct(Data.Models.Product product)
        {
            try
            {
                _db.Products.Add(product);

                var newInventory = new ProductInventory
                {
                    Product = product,
                    QuantityOnHand = 0,
                    IdealQuantity = 10
                };
                _db.ProductInventories.Add(newInventory);
                _db.SaveChanges();
                return new ServiceResponce<Data.Models.Product>
                {
                    Data = product,
                    Time = DateTime.UtcNow,
                    Message = "Saved new product",
                    IsSuccess = true
                };
            }catch(Exception e)
            {
                return new ServiceResponce<Data.Models.Product>
                {
                    Data = product,
                    Time = DateTime.UtcNow,
                    Message =e.Message,
                    IsSuccess = false
                };
            }
        }

        /// <summary>
        /// Retrieves all products from the database
        /// </summary>
        /// <returns>List of all products</returns>

        public List<Data.Models.Product> GetAllProducts()
        {
            return _db.Products.ToList();
        }

        /// <summary>
        /// Retrieves a Product from the database by the product ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Product if found and null otherwise</returns>
        public Data.Models.Product GetProductById(int id)
        {
            return _db.Products.Find(id);
        }
    }
}
