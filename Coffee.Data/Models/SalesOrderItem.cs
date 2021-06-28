using System;
using System.Collections.Generic;
using System.Text;

namespace Coffee.Data.Models
{
    public class SalesOrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public Product Product{ get; set; }
    }
}
