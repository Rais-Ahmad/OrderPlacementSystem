using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class OrderDetails
    {
        public OrderDetails()
        {
            this.Products = new List<Products>();
        }

       public int  OrderID   { get; set; }
       public int  ProductID { get; set; }
       public int  UnitPrice { get; set; }
       public int  Quantity  { get; set; }
       public bool Discount { get; set; }
       public virtual List<Products> Products { get; set; }

    }
}
