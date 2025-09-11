using DesignPatterns.Domain.Entities.Common;
using DesignPatterns.Domain.Interfaces.Behavioral;

namespace DesignPatterns.Domain.Patterns.Behavioral.TemplateMethod
{
    // Template Method Pattern - Define skeleton of algorithm, let subclasses override specific steps

    // Concrete implementations of template method
    public class OnlineOrderProcessor : OrderProcessingTemplate
    {
        protected override string ValidateOrder(Order order)
        {
            var validation = "Online Order Validation:\n";
            validation += "- Email address verified\n";
            validation += "- Credit card validated\n";
            validation += "- Shipping address confirmed\n";
            validation += "✅ Online order validation completed";
            return validation;
        }

        protected override string CalculateTotal(Order order)
        {
            var total = order.Products.Sum(p => p.Price);
            var tax = total * 0.08m; // 8% tax
            var shipping = 5.99m; // Standard online shipping
            order.TotalAmount = total + tax + shipping;
            
            return $"Online Order Total Calculation:\n" +
                   $"- Subtotal: ${total:F2}\n" +
                   $"- Tax (8%): ${tax:F2}\n" +
                   $"- Shipping: ${shipping:F2}\n" +
                   $"- Total: ${order.TotalAmount:F2}";
        }

        protected override string ProcessPayment(Order order)
        {
            return $"Online Payment Processing:\n" +
                   $"- Processing credit card payment of ${order.TotalAmount:F2}\n" +
                   $"- Payment gateway: PayPal/Stripe\n" +
                   $"✅ Payment processed successfully";
        }

        protected override string SendConfirmation(Order order)
        {
            return $"Online Order Confirmation:\n" +
                   $"- Email confirmation sent\n" +
                   $"- SMS notification sent\n" +
                   $"- Order tracking number: TRK{order.Id}2024";
        }
    }

    public class InStoreOrderProcessor : OrderProcessingTemplate
    {
        protected override string ValidateOrder(Order order)
        {
            return "In-Store Order Validation:\n" +
                   "- Customer ID verified\n" +
                   "- Inventory checked in real-time\n" +
                   "✅ In-store order validation completed";
        }

        protected override string CalculateTotal(Order order)
        {
            var total = order.Products.Sum(p => p.Price);
            var tax = total * 0.08m; // 8% tax
            var discount = total * 0.05m; // 5% in-store discount
            order.TotalAmount = total + tax - discount;
            
            return $"In-Store Order Total Calculation:\n" +
                   $"- Subtotal: ${total:F2}\n" +
                   $"- Tax (8%): ${tax:F2}\n" +
                   $"- Store discount (5%): -${discount:F2}\n" +
                   $"- Total: ${order.TotalAmount:F2}";
        }

        protected override string ProcessPayment(Order order)
        {
            return $"In-Store Payment Processing:\n" +
                   $"- Processing payment of ${order.TotalAmount:F2}\n" +
                   $"- Accepting cash/card/mobile payment\n" +
                   $"✅ Payment completed at register";
        }

        protected override string SendConfirmation(Order order)
        {
            return $"In-Store Order Confirmation:\n" +
                   $"- Printed receipt provided\n" +
                   $"- Optional email receipt sent\n" +
                   $"- Receipt #: RCP{order.Id}";
        }
    }

    public class WholesaleOrderProcessor : OrderProcessingTemplate
    {
        protected override string ValidateOrder(Order order)
        {
            return "Wholesale Order Validation:\n" +
                   "- Business license verified\n" +
                   "- Credit limit checked\n" +
                   "- Minimum order quantity validated\n" +
                   "✅ Wholesale order validation completed";
        }

        protected override string CalculateTotal(Order order)
        {
            var total = order.Products.Sum(p => p.Price);
            var wholesaleDiscount = total * 0.20m; // 20% wholesale discount
            var tax = (total - wholesaleDiscount) * 0.08m; // Tax after discount
            order.TotalAmount = total - wholesaleDiscount + tax;
            
            return $"Wholesale Order Total Calculation:\n" +
                   $"- Subtotal: ${total:F2}\n" +
                   $"- Wholesale discount (20%): -${wholesaleDiscount:F2}\n" +
                   $"- Tax (8%): ${tax:F2}\n" +
                   $"- Total: ${order.TotalAmount:F2}";
        }

        protected override string ProcessPayment(Order order)
        {
            return $"Wholesale Payment Processing:\n" +
                   $"- Net 30 payment terms applied\n" +
                   $"- Invoice amount: ${order.TotalAmount:F2}\n" +
                   $"✅ Invoice generated and sent";
        }

        protected override string SendConfirmation(Order order)
        {
            return $"Wholesale Order Confirmation:\n" +
                   $"- Purchase order confirmation sent\n" +
                   $"- Account manager notified\n" +
                   $"- Delivery scheduled within 3-5 business days\n" +
                   $"- PO Number: PO{order.Id}";
        }
    }

    // Data Mining Template Example
    public abstract class DataMiningTemplate
    {
        public string MineData(string dataSource)
        {
            var result = "Data Mining Process:\n";
            result += ExtractData(dataSource) + "\n";
            result += ParseData() + "\n";
            result += AnalyzeData() + "\n";
            result += GenerateReport() + "\n";
            return result;
        }

        protected abstract string ExtractData(string source);
        protected abstract string ParseData();
        protected abstract string AnalyzeData();
        
        protected virtual string GenerateReport()
        {
            return "✅ Standard report generated";
        }
    }

    public class CSVDataMiner : DataMiningTemplate
    {
        protected override string ExtractData(string source)
        {
            return $"📊 Extracting data from CSV file: {source}";
        }

        protected override string ParseData()
        {
            return "🔄 Parsing CSV format with comma separators";
        }

        protected override string AnalyzeData()
        {
            return "📈 Performing statistical analysis on numerical columns";
        }
    }

    public class XMLDataMiner : DataMiningTemplate
    {
        protected override string ExtractData(string source)
        {
            return $"📊 Extracting data from XML file: {source}";
        }

        protected override string ParseData()
        {
            return "🔄 Parsing XML structure with DOM parser";
        }

        protected override string AnalyzeData()
        {
            return "📈 Analyzing hierarchical data relationships";
        }

        protected override string GenerateReport()
        {
            return "✅ Detailed XML analysis report with schema validation";
        }
    }

    public class DatabaseDataMiner : DataMiningTemplate
    {
        protected override string ExtractData(string source)
        {
            return $"📊 Extracting data from database: {source}";
        }

        protected override string ParseData()
        {
            return "🔄 Executing SQL queries and result set processing";
        }

        protected override string AnalyzeData()
        {
            return "📈 Performing database analytics with aggregation functions";
        }
    }

    // Template Method Service
    public class TemplateMethodService
    {
        public List<string> DemonstrateOrderProcessing()
        {
            var results = new List<string>();
            
            var products = new List<Product>
            {
                new() { Id = 1, Name = "Laptop", Price = 999.99m },
                new() { Id = 2, Name = "Mouse", Price = 29.99m },
                new() { Id = 3, Name = "Keyboard", Price = 79.99m }
            };

            var order = new Order
            {
                Id = 2024001,
                UserId = 1,
                Products = products,
                OrderDate = DateTime.UtcNow,
                Status = "New"
            };

            var processors = new List<(string type, OrderProcessingTemplate processor)>
            {
                ("Online", new OnlineOrderProcessor()),
                ("In-Store", new InStoreOrderProcessor()),
                ("Wholesale", new WholesaleOrderProcessor())
            };

            foreach (var (type, processor) in processors)
            {
                results.Add($"=== {type} Order Processing ===");
                var result = processor.ProcessOrder(order);
                results.Add(result);
                results.Add($"Final total: ${order.TotalAmount:F2}\n");
            }

            return results;
        }

        public List<string> DemonstrateDataMining()
        {
            var results = new List<string>();
            
            var miners = new List<(string type, DataMiningTemplate miner)>
            {
                ("CSV", new CSVDataMiner()),
                ("XML", new XMLDataMiner()),
                ("Database", new DatabaseDataMiner())
            };

            foreach (var (type, miner) in miners)
            {
                results.Add($"=== {type} Data Mining ===");
                var result = miner.MineData($"sample_data.{type.ToLower()}");
                results.Add(result);
            }

            return results;
        }

        public string ProcessCustomOrder(string orderType, List<(string name, decimal price)> items)
        {
            var products = items.Select((item, index) => new Product
            {
                Id = index + 1,
                Name = item.name,
                Price = item.price
            }).ToList();

            var order = new Order
            {
                Id = new Random().Next(100000, 999999),
                Products = products,
                OrderDate = DateTime.UtcNow
            };

            OrderProcessingTemplate processor = orderType.ToLower() switch
            {
                "online" => new OnlineOrderProcessor(),
                "instore" => new InStoreOrderProcessor(),
                "wholesale" => new WholesaleOrderProcessor(),
                _ => new OnlineOrderProcessor()
            };

            return processor.ProcessOrder(order);
        }
    }
}