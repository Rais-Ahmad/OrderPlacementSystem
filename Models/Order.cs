using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Order
    {
        public Order()
        {
            this.OrderDetails = new List<OrderDetails>();
        }
        public int OrderID { get; set; }
        public string CustomerID { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipRegion { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }

        public virtual List<OrderDetails> OrderDetails { get; set; }

        public virtual Customer1 Customer { get; set; }

        public virtual Order Orders1 { get; set; }
        public virtual Order Order1 { get; set; }
        public virtual Order Orders11 { get; set; }
        public virtual Order Order2 { get; set; }
        public virtual Order Orders12 { get; set; }
        public virtual Order Order3 { get; set; }
    }
}