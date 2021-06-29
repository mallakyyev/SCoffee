using SCoffee.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Coffee.Data.Models;

namespace SCoffee.Serialization
{
    public static class ProductMapper
    {

        /// <summary>
        /// Maps Product Model into Product View Model, We can use IMapper interface, but explicit code can come in handy in many cases
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static ProductModel SerialiazeProductModel(Product product)
        {
            return new ProductModel
            {
                Id = product.Id,
                CreatedOn = product.CreatedOn,
                UpdatedOn = product.UpdatedOn,
                Price = (decimal)product.Price,
                Name = product.Name,
                Description = product.Description,
                IsTaxable = product.IsTaxable,
                IsArchived = product.IsArchived
            };
        }

        /// <summary>
        /// Maps Product View Model into Product Model, We can use IMapper interface, but explicit code can come in handy in many cases
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>

        public static Product SerialiazeProductModel(ProductModel product)
        {
            return new Product
            {
                Id = product.Id,
                CreatedOn = product.CreatedOn,
                UpdatedOn = product.UpdatedOn,
                Price = (double)product.Price,
                Name = product.Name,
                Description = product.Description,
                IsTaxable = product.IsTaxable,
                IsArchived = product.IsArchived
            };
        }



    }
}
