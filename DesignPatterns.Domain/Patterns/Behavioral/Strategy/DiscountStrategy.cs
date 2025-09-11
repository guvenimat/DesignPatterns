using DesignPatterns.Domain.Interfaces.Behavioral;

namespace DesignPatterns.Domain.Patterns.Behavioral.Strategy
{
    // Strategy Pattern - Define family of algorithms and make them interchangeable

    // Concrete Strategies for Discount Calculation
    public class RegularCustomerDiscount : IDiscountStrategy
    {
        public decimal ApplyDiscount(decimal amount)
        {
            return amount * 0.95m; // 5% discount
        }

        public string GetDiscountDescription()
        {
            return "Regular Customer: 5% discount";
        }
    }

    public class PremiumCustomerDiscount : IDiscountStrategy
    {
        public decimal ApplyDiscount(decimal amount)
        {
            return amount * 0.90m; // 10% discount
        }

        public string GetDiscountDescription()
        {
            return "Premium Customer: 10% discount";
        }
    }

    public class VipCustomerDiscount : IDiscountStrategy
    {
        public decimal ApplyDiscount(decimal amount)
        {
            return amount * 0.80m; // 20% discount
        }

        public string GetDiscountDescription()
        {
            return "VIP Customer: 20% discount";
        }
    }

    public class HolidayDiscount : IDiscountStrategy
    {
        private readonly decimal _discountRate;

        public HolidayDiscount(decimal discountRate)
        {
            _discountRate = discountRate;
        }

        public decimal ApplyDiscount(decimal amount)
        {
            return amount * (1 - _discountRate);
        }

        public string GetDiscountDescription()
        {
            return $"Holiday Special: {_discountRate * 100:F0}% discount";
        }
    }

    public class NoDiscount : IDiscountStrategy
    {
        public decimal ApplyDiscount(decimal amount)
        {
            return amount;
        }

        public string GetDiscountDescription()
        {
            return "No discount applied";
        }
    }

    // Context class that uses strategies
    public class ShoppingCart
    {
        private readonly List<CartItem> _items = new();
        private IDiscountStrategy _discountStrategy = new NoDiscount();

        public void AddItem(string name, decimal price, int quantity = 1)
        {
            _items.Add(new CartItem(name, price, quantity));
        }

        public void SetDiscountStrategy(IDiscountStrategy strategy)
        {
            _discountStrategy = strategy;
        }

        public decimal GetSubtotal()
        {
            return _items.Sum(item => item.TotalPrice);
        }

        public decimal GetTotal()
        {
            var subtotal = GetSubtotal();
            return _discountStrategy.ApplyDiscount(subtotal);
        }

        public decimal GetDiscountAmount()
        {
            var subtotal = GetSubtotal();
            return subtotal - GetTotal();
        }

        public string GetDiscountDescription()
        {
            return _discountStrategy.GetDiscountDescription();
        }

        public List<string> GetItems()
        {
            return _items.Select(item => $"{item.Name} x{item.Quantity} - ${item.TotalPrice:F2}").ToList();
        }

        public void Clear()
        {
            _items.Clear();
        }

        public string GetCartSummary()
        {
            var summary = "=== Shopping Cart Summary ===\n";
            summary += string.Join("\n", GetItems()) + "\n";
            summary += $"Subtotal: ${GetSubtotal():F2}\n";
            summary += $"Discount: {GetDiscountDescription()}\n";
            summary += $"Discount Amount: -${GetDiscountAmount():F2}\n";
            summary += $"Total: ${GetTotal():F2}";
            return summary;
        }
    }

    public class CartItem
    {
        public string Name { get; }
        public decimal Price { get; }
        public int Quantity { get; }
        public decimal TotalPrice => Price * Quantity;

        public CartItem(string name, decimal price, int quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }
    }

    // Payment Strategy
    public interface IPaymentStrategy
    {
        bool ProcessPayment(decimal amount);
        string GetPaymentDescription();
    }

    public class CreditCardPayment : IPaymentStrategy
    {
        private readonly string _cardNumber;

        public CreditCardPayment(string cardNumber)
        {
            _cardNumber = cardNumber;
        }

        public bool ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Processing credit card payment of ${amount:F2} using card ****{_cardNumber.Substring(_cardNumber.Length - 4)}");
            return true;
        }

        public string GetPaymentDescription()
        {
            return $"Credit Card ending in {_cardNumber.Substring(_cardNumber.Length - 4)}";
        }
    }

    public class PayPalPayment : IPaymentStrategy
    {
        private readonly string _email;

        public PayPalPayment(string email)
        {
            _email = email;
        }

        public bool ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Processing PayPal payment of ${amount:F2} for account {_email}");
            return true;
        }

        public string GetPaymentDescription()
        {
            return $"PayPal account: {_email}";
        }
    }

    public class CashPayment : IPaymentStrategy
    {
        public bool ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Processing cash payment of ${amount:F2}");
            return true;
        }

        public string GetPaymentDescription()
        {
            return "Cash payment";
        }
    }

    // Strategy Service
    public class StrategyService
    {
        public List<string> DemonstrateDiscountStrategies()
        {
            var results = new List<string>();
            var cart = new ShoppingCart();

            // Add items to cart
            cart.AddItem("Laptop", 1000.00m);
            cart.AddItem("Mouse", 25.00m, 2);
            cart.AddItem("Keyboard", 75.00m);

            // Test different discount strategies
            var strategies = new List<IDiscountStrategy>
            {
                new NoDiscount(),
                new RegularCustomerDiscount(),
                new PremiumCustomerDiscount(),
                new VipCustomerDiscount(),
                new HolidayDiscount(0.15m) // 15% holiday discount
            };

            foreach (var strategy in strategies)
            {
                cart.SetDiscountStrategy(strategy);
                results.Add(cart.GetCartSummary());
                results.Add(""); // Empty line for separation
            }

            return results;
        }

        public List<string> DemonstratePaymentStrategies()
        {
            var results = new List<string>();
            var amount = 1125.00m;

            var paymentMethods = new List<IPaymentStrategy>
            {
                new CreditCardPayment("1234567890123456"),
                new PayPalPayment("user@example.com"),
                new CashPayment()
            };

            results.Add($"Processing payment of ${amount:F2} using different payment methods:\n");

            foreach (var method in paymentMethods)
            {
                using var stringWriter = new StringWriter();
                var originalOut = Console.Out;
                Console.SetOut(stringWriter);

                var success = method.ProcessPayment(amount);

                Console.SetOut(originalOut);
                
                results.Add($"Method: {method.GetPaymentDescription()}");
                results.Add($"Result: {(success ? "Success" : "Failed")}");
                results.Add(stringWriter.ToString().Trim());
                results.Add(""); // Empty line
            }

            return results;
        }

        public string CreateCustomScenario(List<(string name, decimal price, int quantity)> items, string customerType)
        {
            var cart = new ShoppingCart();

            foreach (var (name, price, quantity) in items)
            {
                cart.AddItem(name, price, quantity);
            }

            IDiscountStrategy strategy = customerType.ToLower() switch
            {
                "regular" => new RegularCustomerDiscount(),
                "premium" => new PremiumCustomerDiscount(),
                "vip" => new VipCustomerDiscount(),
                "holiday" => new HolidayDiscount(0.25m),
                _ => new NoDiscount()
            };

            cart.SetDiscountStrategy(strategy);
            return cart.GetCartSummary();
        }
    }
}