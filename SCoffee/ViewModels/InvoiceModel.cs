using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCoffee.ViewModels
{
    /// <summary>
    /// View Model for Open Sales Order
    /// </summary>
    public class InvoiceModel
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int CustomerId { get; set; }
        public List<SalesOrderItemModel> LineItems{get;set;}

    }


    /// <summary>
    /// View Model for Sales order item
    /// </summary>
    public class SalesOrderItemModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public ProductModel Product { get; set; }

    }
}
