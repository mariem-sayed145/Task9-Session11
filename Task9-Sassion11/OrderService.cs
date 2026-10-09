using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task9_Sassion11
{
    public class OrderService
    {
        public event Action<Order> OrderProcessed;

        public void ProcessOrder(Order order)
        {
            Console.WriteLine($"Processing Order {order.Id}...");

            Console.WriteLine($"Order {order.Id} completed.");

            OrderProcessed?.Invoke(order);
        }

    }
}
