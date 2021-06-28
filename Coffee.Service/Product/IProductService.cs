using System;
using System.Collections.Generic;
using System.Text;

namespace Coffee.Service.Product
{
    public interface IProductService
    {
        List<Data.Models.Product> GetAllProducts();
        Data.Models.Product GetProductById(int id);
        ServiceResponce<Data.Models.Product> CreateProduct(Data.Models.Product product);
        ServiceResponce<Data.Models.Product> ArchiveProduct(int id);
    }
}
