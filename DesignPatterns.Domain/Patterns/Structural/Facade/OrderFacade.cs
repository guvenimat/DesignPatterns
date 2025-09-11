using DesignPatterns.Domain.Entities.Common;
using DesignPatterns.Domain.Interfaces.Structural;

namespace DesignPatterns.Domain.Patterns.Structural.Facade
{
    // Complex subsystems
    public class InventoryService
    {
        public bool CheckAvailability(List<Product> products)
        {
            // Simulate inventory check
            Console.WriteLine("Checking inventory for products...");
            return products.All(p => p.Id > 0);
        }

        public void ReserveProducts(List<Product> products)
        {
            Console.WriteLine($"Reserved {products.Count} products in inventory");
        }
    }

    public class PaymentService
    {
        public bool ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Processing payment of ${amount:F2}");
            return amount > 0;
        }
    }

    public class ShippingService
    {
        public string CalculateShipping(User user, List<Product> products)
        {
            var shippingCost = products.Count * 2.99m;
            Console.WriteLine($"Calculated shipping: ${shippingCost:F2}");
            return $"Standard shipping: ${shippingCost:F2}";
        }

        public void ArrangeShipping(User user, List<Product> products)
        {
            Console.WriteLine($"Arranged shipping for {products.Count} products to {user.Email}");
        }
    }

    public class NotificationService
    {
        public void SendOrderConfirmation(User user, Order order)
        {
            Console.WriteLine($"Order confirmation sent to {user.Email} for order #{order.Id}");
        }
    }

    // Facade Pattern - Simplifies complex subsystem
    public class OrderFacade : IOrderFacade
    {
        private readonly InventoryService _inventoryService;
        private readonly PaymentService _paymentService;
        private readonly ShippingService _shippingService;
        private readonly NotificationService _notificationService;

        public OrderFacade()
        {
            _inventoryService = new InventoryService();
            _paymentService = new PaymentService();
            _shippingService = new ShippingService();
            _notificationService = new NotificationService();
        }

        public bool PlaceOrder(User user, List<Product> products)
        {
            try
            {
                // Step 1: Check inventory
                if (!_inventoryService.CheckAvailability(products))
                {
                    Console.WriteLine("Order failed: Products not available");
                    return false;
                }

                // Step 2: Calculate total
                var total = products.Sum(p => p.Price);
                var shippingInfo = _shippingService.CalculateShipping(user, products);

                // Step 3: Process payment
                if (!_paymentService.ProcessPayment(total))
                {
                    Console.WriteLine("Order failed: Payment processing failed");
                    return false;
                }

                // Step 4: Reserve inventory
                _inventoryService.ReserveProducts(products);

                // Step 5: Arrange shipping
                _shippingService.ArrangeShipping(user, products);

                // Step 6: Create order and send confirmation
                var order = new Order
                {
                    Id = new Random().Next(1000, 9999),
                    UserId = user.Id,
                    Products = products,
                    TotalAmount = total,
                    OrderDate = DateTime.UtcNow,
                    Status = "Confirmed"
                };

                _notificationService.SendOrderConfirmation(user, order);

                Console.WriteLine($"Order placed successfully! Order ID: {order.Id}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Order failed: {ex.Message}");
                return false;
            }
        }

        public string GetOrderSummary(User user, List<Product> products)
        {
            var total = products.Sum(p => p.Price);
            var shippingInfo = _shippingService.CalculateShipping(user, products);
            var summary = $"Order Summary for {user.Username}:\n";
            summary += $"Products: {products.Count} items\n";
            summary += $"Subtotal: ${total:F2}\n";
            summary += $"Shipping: {shippingInfo}\n";
            summary += $"Total: ${total + (products.Count * 2.99m):F2}";
            return summary;
        }
    }
}