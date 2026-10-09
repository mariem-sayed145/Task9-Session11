using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task9_Sassion11
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; } 

      
        public override string ToString()
        {
            return $"Order Id: {Id}, Customer Name: {CustomerName}, Price: {Price}, Quantity: {Quantity}";
        }


    }
}
