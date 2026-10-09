using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Task9_Sassion11
{
    internal class Program
    {

        // User-defined Delegate for Order Price Calculation

        public delegate decimal PriceCalculator(Order order);

        public static decimal CalculateOrderPrice(Order order, PriceCalculator calculator)
        {
            return calculator(order);
        }

        public static decimal CalculateTotal(Order order)
        {
            return order.Price * order.Quantity;
        }

        public static decimal CalculateTotalWithDiscount(Order order)
        {
            decimal total = order.Price * order.Quantity;

            return total - 50m;
        }

        // Built-in Delegate for Order Price Calculation func<>
        public static decimal CalculateOrderPrice2(
        Order order,
        Func<Order, decimal> calculator)
        {
            return calculator(order);
        }


        // User-defined Delegate for Order Validation
        public static bool ValidateOrder(
        Order order,
        Predicate<Order> validationRule)
        {
            return validationRule(order);
        }

        // Built-in Delegate for Order Processing Action<Order>
        public static void ProcessOrder(
        Order order,
        Action<Order> action)
        {
            Console.WriteLine($"Processing Order {order.Id}...");
            action(order);
        }

        // Handlers for Order Processing 
        static void PrintOrderMessage(Order order)
        {
            Console.WriteLine(
            $"Handler 1: Order {order.Id} has been processed.");
        }

        static void SendNotification(Order order)
        {
            Console.WriteLine(
                $"Handler 2: Notification sent for Order {order.Id}.");
        }

        static void WriteAuditMessage(Order order)
        {
            Console.WriteLine(
                $"Handler 3: Audit recorded for Order {order.Id}.");

        }

        // EventHandler for Order Processing
        static void Handler1(Order order)
        {
            Console.WriteLine($"Handler1: Order {order.Id} processed.");
        }

        static void Handler2(Order order)
        {
            Console.WriteLine($"Handler2: Order {order.Id} processed.");
        }
        static void Main(string[] args)
        {

            Console.WriteLine("*******************************************************");
            Console.WriteLine("***************** Section 1 ***************************");
            Console.WriteLine("*******************************************************");

            // Create new Books objects

            List<Book> Books = new List<Book>
            {
                new Book(
                    "9781",
                    "C# Basics",
                    new string[] { "Ahmed", "Mona" },
                    new DateTime(2025, 1, 1),
                    250m)

                , new Book(
                    "9782",
                    "C# Advanced",
                    new string[] { "Ali", "Sara" },
                    new DateTime(2025, 2, 1),
                    300m)

                , new Book(
                    "9783",
                    "C# Expert",
                    new string[] { "Omar", "Laila" },
                    new DateTime(2025, 3, 1),
                    350m)
            };

            foreach (Book B in Books)
            {
                Console.WriteLine(B);
            }


            Console.WriteLine("=============================================================");

            // User-defined Delegate 

            Console.WriteLine("A) User-defined Delegate"); 
            Console.WriteLine("-----------------------"); 
            LibraryEngine.ProcessBooks(Books, BookFunctions.GetTitle);


            Console.WriteLine("=============================================================");


            // Built-in Delegate: Func<Book, string> 

            Console.WriteLine("\nB) Built-in Delegate - Func<Book, string>"); 
            Console.WriteLine("-----------------------------------------"); 
            Func<Book, string> getAuthors = BookFunctions.GetAuthors; 
            
            foreach (Book book in Books) 
            { 
                Console.WriteLine(getAuthors(book)); 
            }


            // Anonymous Method: GetISBN 

            Console.WriteLine("=============================================================");
            Console.WriteLine("\nC) Anonymous Method - GetISBN");
            Console.WriteLine("-----------------------------------------");

            Func<Book, string> getISBN = delegate (Book book) 
            { 
                return book.ISBN; 
            }; 

            foreach (Book book in Books) 
            { 
                Console.WriteLine(getISBN(book)); 
            }


            // Lambda Expression: GetPublicationDate

            Console.WriteLine("=============================================================");
            Console.WriteLine("\nD) Lambda Expression - GetPublicationDate");
            Console.WriteLine("-----------------------------------------");

            Func<Book, DateTime> getPublicationDate = book => book.PublicationDate;

            foreach (Book book in Books)
            {
                Console.WriteLine(getPublicationDate(book));
            }

            Console.WriteLine("\n=============================================================");


            // Order 

            Console.WriteLine("*******************************************************");
            Console.WriteLine("***************** Section 2 ***************************");
            Console.WriteLine("*******************************************************");

            Order order = new Order
            {
                Id = 101,
                CustomerName = "Mariam",
                Price = 200m,
                Quantity = 3
            };


            decimal normalPrice = CalculateOrderPrice(order,CalculateTotal);
            
            decimal discountedPrice = CalculateOrderPrice(order, CalculateTotalWithDiscount); 

            Console.WriteLine($"Normal Price: {normalPrice}"); 
            Console.WriteLine($"Discounted Price: {discountedPrice}");


            Console.WriteLine("\n=============================================================");

            // Using built-in delegate Func<Order, decimal> to calculate order price

            Func<Order, decimal> normalCalculator =  (x) => x.Price * x.Quantity;

            normalCalculator(order);

            Console.WriteLine(normalCalculator(order));

            Func<Order, decimal> discountedCalculator = (x) => (x.Price * x.Quantity) - 50m;

            discountedCalculator(order);
            Console.WriteLine(discountedCalculator(order));

            Console.WriteLine("\n=============================================================");

            decimal normalPrice2 = CalculateOrderPrice2(order, normalCalculator); 
            decimal discountedPrice2 = CalculateOrderPrice2(order, discountedCalculator); 
            Console.WriteLine($"Normal Price: {normalPrice2}"); 
            Console.WriteLine($"Discounted Price: {discountedPrice2}");

            Console.WriteLine("\n=============================================================");

            // Using Predicate<Order> to validate order


            bool validQuantity = ValidateOrder(order, order2 => order2.Quantity > 0);
            
            bool validPrice = ValidateOrder(order, order2 => order2.Price > 0); 

            bool validCustomerName = ValidateOrder(order, order2 => !string.IsNullOrWhiteSpace(order2.CustomerName)); 

            Console.WriteLine($"Valid Quantity: {validQuantity}"); 
            Console.WriteLine($"Valid Price: {validPrice}"); 
            Console.WriteLine($"Valid Customer Name: {validCustomerName}");

            Console.WriteLine("\n=============================================================");

            // Using Action<Order> to print order details

            Action<Order> printOrder = order3 => 
            { 
                Console.WriteLine($"Order ID: {order.Id}"); 
                Console.WriteLine($"Customer: {order.CustomerName}"); 
                Console.WriteLine($"Price: {order.Price}"); 
                Console.WriteLine($"Quantity: {order.Quantity}\n"); 
            }; 

            Action<Order> sendConfirmation = order3 => 
            { 
                Console.WriteLine(
                    $"Confirmation message sent to {order.CustomerName} \n" 
                    + $"for Order {order.Id}.\n"); 
            }; 

            Action<Order> writeAudit = order3 => 
            { 
                Console.WriteLine($"AUDIT: Order {order.Id} was processed at {DateTime.Now}.");
            }; 

            ProcessOrder(order, printOrder); 
            ProcessOrder(order, sendConfirmation); 
            ProcessOrder(order, writeAudit);

            Console.WriteLine("\n=============================================================");

            // Using EventHandler to handle order processing events

            OrderService orderService = new OrderService(); 
            orderService.OrderProcessed += PrintOrderMessage; 
            orderService.OrderProcessed += SendNotification; 
            orderService.OrderProcessed += WriteAuditMessage; 
            orderService.ProcessOrder(order);


            OrderService orderService2 = new OrderService(); 
            // Subscribe both handlers.
            orderService2.OrderProcessed += Handler1; 
            orderService2.OrderProcessed += Handler2; 
            Console.WriteLine("First processing:"); 
            
            orderService2.ProcessOrder(order); 
            orderService2.OrderProcessed -= Handler1; 
            Console.WriteLine("\nSecond processing:");
            orderService2.ProcessOrder(order);


            // =============================================================
            // Part 7 — Final Application
            // =============================================================

            Console.WriteLine("\n*******************************************************");
            Console.WriteLine("***************** Part 7: Final Application **********");
            Console.WriteLine("*******************************************************");

            // 1. Create Order

            Order finalOrder = new Order
            {
                Id = 102,
                CustomerName = "Mariam",
                Price = 200m,
                Quantity = 3
            };

            Console.WriteLine("\nStep 1: Order Created");
            Console.WriteLine($"Order ID: {finalOrder.Id}");
            Console.WriteLine($"Customer: {finalOrder.CustomerName}");

            // 2. Validate Order using Predicate<Order>

            Console.WriteLine("\nStep 2: Validate Order");

            Predicate<Order> validateOrder = o =>
                o.Quantity > 0 &&
                o.Price > 0 &&
                !string.IsNullOrWhiteSpace(o.CustomerName);

            bool isValid = ValidateOrder(finalOrder, validateOrder);

            Console.WriteLine($"Order Valid: {isValid}");

            if (!isValid)
            {
                Console.WriteLine("Order validation failed.");
                return;
            }

            // 3. Calculate Price using User-defined Delegate

            Console.WriteLine("\nStep 3: Calculate Price");

            PriceCalculator priceCalculator = CalculateTotal;

            decimal totalPrice = CalculateOrderPrice(
                finalOrder,
                priceCalculator
            );

            Console.WriteLine($"Total Price: {totalPrice}");

            // 4. Demonstrate Func<Order, decimal>

            Func<Order, decimal> finalCalculator =
                o => o.Price * o.Quantity;

            decimal calculatedPrice = CalculateOrderPrice2(
                finalOrder,
                finalCalculator
            );

            Console.WriteLine($"Price using Func: {calculatedPrice}");

            // 5. Process Order using Action<Order>

            Console.WriteLine("\nStep 4: Process Order");

            Action<Order> processAction = o =>
            {
                Console.WriteLine($"Processing Order {o.Id}...");
                Console.WriteLine($"Customer: {o.CustomerName}");
                Console.WriteLine($"Total Amount: {o.Price * o.Quantity}");
            };

            ProcessOrder(finalOrder, processAction);

            // 6. Subscribe Multiple Handlers to the Event

            Console.WriteLine("\nStep 5: Subscribe Event Handlers");

            OrderService finalOrderService = new OrderService();

            finalOrderService.OrderProcessed += PrintOrderMessage;
            finalOrderService.OrderProcessed += SendNotification;
            finalOrderService.OrderProcessed += WriteAuditMessage;

            // 7. Raise the Event and Execute All Subscribers

            Console.WriteLine("\nStep 6: Raise OrderProcessed Event");

            finalOrderService.ProcessOrder(finalOrder);




        }
    }
}

