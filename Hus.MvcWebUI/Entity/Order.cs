using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hus.MvcWebUI.Entity
{

    //basic information of the order
    public class Order
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public double Total { get; set; }
        public DateTime OrderDate { get; set; }
        public EnumOrderState OrderState { get; set; }

        public string Username { get; set; }
        public string AddressTitle { get; set; }    
        public string Address { get; set; }
        public string City { get; set; }   
        public string District { get; set; }
        public string Neighborhood { get; set; } 
        public string PostalCode { get; set; }

        public virtual List<OrderLine> OrderLines { get; set; }
    }
    //Detail of order
    public class OrderLine
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        public int Quantity { get; set; }
        public double Price { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }


    }
}