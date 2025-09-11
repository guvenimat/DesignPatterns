using DesignPatterns.Domain.Patterns.Structural.Adapter;
using DesignPatterns.Domain.Patterns.Structural.Bridge;
using DesignPatterns.Domain.Patterns.Structural.Composite;
using DesignPatterns.Domain.Patterns.Structural.Decorator;
using DesignPatterns.Domain.Patterns.Structural.Facade;
using DesignPatterns.Domain.Patterns.Structural.Flyweight;
using DesignPatterns.Domain.Patterns.Structural.Proxy;
using DesignPatterns.Application.Interfaces;
using DesignPatterns.Domain.Entities.Common;
using DesignPatterns.Domain.Constants;

namespace DesignPatterns.Application.Services
{
    public class StructuralPatternsService : IStructuralPatternsService
    {
        private readonly DesignPatterns.Domain.Patterns.Structural.Adapter.PaymentService _paymentService;
        private readonly DesignPatterns.Domain.Patterns.Structural.Bridge.NotificationService _notificationService;
        private readonly FileSystemService _fileSystemService;
        private readonly CoffeeShopService _coffeeShopService;
        private readonly OrderFacade _orderFacade;
        private readonly TextEditorService _textEditorService;
        private readonly ImageGalleryService _imageGalleryService;

        public StructuralPatternsService()
        {
            _paymentService = new DesignPatterns.Domain.Patterns.Structural.Adapter.PaymentService();
            _notificationService = new DesignPatterns.Domain.Patterns.Structural.Bridge.NotificationService();
            _fileSystemService = new FileSystemService();
            _coffeeShopService = new CoffeeShopService();
            _orderFacade = new OrderFacade();
            _textEditorService = new TextEditorService();
            _imageGalleryService = new ImageGalleryService();
        }

        // Adapter Pattern Demo
        public object DemonstrateAdapter()
        {
            var testAmounts = new decimal[] { 100.50m, 250.00m, 999.99m };
            var results = new List<object>();

            foreach (var amount in testAmounts)
            {
                var paymentResults = _paymentService.ProcessPaymentWithAllSystems(amount);
                results.Add(new
                {
                    Amount = amount,
                    PaymentResults = paymentResults
                });
            }

            return new
            {
                Pattern = "Adapter",
                Description = "Allows incompatible interfaces to work together",
                Results = results,
                Example = "Adapting legacy payment system to work with modern payment interface",
                Benefits = new[]
                {
                    "Reuse existing code",
                    "Separate interface conversion from business logic",
                    "Follow Single Responsibility Principle"
                }
            };
        }

        // Bridge Pattern Demo
        public object DemonstrateBridge()
        {
            var messageTypes = new[] { "urgent", "regular", "marketing" };
            var testMessage = "System maintenance scheduled for tonight";
            var results = new List<object>();

            foreach (var messageType in messageTypes)
            {
                var notificationResults = _notificationService.SendNotificationThroughAllChannels(testMessage, messageType);
                results.Add(new
                {
                    MessageType = messageType,
                    Message = testMessage,
                    Results = notificationResults
                });
            }

            return new
            {
                Pattern = "Bridge",
                Description = "Separates abstraction from implementation",
                Results = results,
                Example = "Different notification types sent through various channels (Email, SMS, Push)",
                Benefits = new[]
                {
                    "Abstraction and implementation can vary independently",
                    "Runtime switching of implementations",
                    "Platform independence"
                }
            };
        }

        // Composite Pattern Demo
        public object DemonstrateComposite()
        {
            var fileSystem = _fileSystemService.CreateSampleFileSystem();
            var fileSystemInfo = _fileSystemService.GetFileSystemInfo(fileSystem);
            var totalSize = _fileSystemService.CalculateTotalSize(fileSystem);
            var pdfFiles = _fileSystemService.FindFiles(fileSystem, ".pdf");
            var txtFiles = _fileSystemService.FindFiles(fileSystem, ".txt");

            return new
            {
                Pattern = "Composite",
                Description = "Composes objects into tree structures to represent hierarchies",
                FileSystemStructure = fileSystemInfo,
                TotalSize = $"{totalSize} KB",
                FileSearch = new
                {
                    PdfFiles = pdfFiles,
                    TxtFiles = txtFiles
                },
                Example = "File system with folders and files - both treated uniformly",
                Benefits = new[]
                {
                    "Uniform treatment of individual and composite objects",
                    "Easy to add new component types",
                    "Simplified client code"
                }
            };
        }

        // Decorator Pattern Demo
        public object DemonstrateDecorator()
        {
            var menu = _coffeeShopService.GetCoffeeMenu();
            
            var customOrders = new List<object>
            {
                new { AddOns = new List<string>(), Order = _coffeeShopService.OrderCoffee(new List<string>()) },
                new { AddOns = new List<string> { "milk" }, Order = _coffeeShopService.OrderCoffee(new List<string> { "milk" }) },
                new { AddOns = new List<string> { "milk", "sugar", "vanilla" }, Order = _coffeeShopService.OrderCoffee(new List<string> { "milk", "sugar", "vanilla" }) },
                new { AddOns = new List<string> { "whipcream", "caramel" }, Order = _coffeeShopService.OrderCoffee(new List<string> { "whipcream", "caramel" }) }
            };

            return new
            {
                Pattern = "Decorator",
                Description = "Adds behavior to objects dynamically without altering structure",
                CoffeeMenu = menu,
                CustomOrders = customOrders,
                Example = "Coffee with various add-ons (milk, sugar, whip cream, etc.)",
                Benefits = new[]
                {
                    "Add responsibilities to objects dynamically",
                    "More flexible than inheritance",
                    "Compose behaviors by wrapping objects"
                }
            };
        }

        // Facade Pattern Demo
        public object DemonstrateFacade()
        {
            var user = new User
            {
                Id = 1,
                Username = "john_doe",
                Email = "john@example.com",
                Role = "Customer",
                IsActive = true
            };

            var products = new List<Product>
            {
                new() { Id = 1, Name = "Laptop", Price = 999.99m, Category = "Electronics" },
                new() { Id = 2, Name = "Mouse", Price = 25.99m, Category = "Electronics" },
                new() { Id = 3, Name = "Book", Price = 15.99m, Category = "Books" }
            };

            var orderSummary = _orderFacade.GetOrderSummary(user, products);
            var orderResult = _orderFacade.PlaceOrder(user, products);

            return new
            {
                Pattern = "Facade",
                Description = "Provides simplified interface to complex subsystem",
                User = user,
                Products = products,
                OrderSummary = orderSummary,
                OrderPlaced = orderResult,
                Example = "Order placement involves inventory, payment, shipping, and notification subsystems",
                Benefits = new[]
                {
                    "Simplifies complex subsystems",
                    "Reduces coupling between client and subsystem",
                    "Provides clean interface"
                }
            };
        }

        // Flyweight Pattern Demo
        public object DemonstrateFlyweight()
        {
            var renderOutput = _textEditorService.RenderDocument();
            var flyweightInfo = _textEditorService.GetFlyweightInfo();

            return new
            {
                Pattern = "Flyweight",
                Description = "Minimizes memory usage by sharing efficiently among similar objects",
                DocumentRender = renderOutput,
                FlyweightObjects = flyweightInfo,
                Example = "Text editor sharing character fonts to save memory",
                Benefits = new[]
                {
                    "Reduces memory consumption",
                    "Improves performance for large numbers of objects",
                    "Separates intrinsic and extrinsic state"
                }
            };
        }

        // Proxy Pattern Demo
        public object DemonstrateProxy()
        {
            var virtualProxyDemo = _imageGalleryService.DemonstrateVirtualProxy();
            var securityProxyDemo = _imageGalleryService.DemonstrateSecurityProxy();
            var cachingProxyDemo = _imageGalleryService.DemonstrateCachingProxy();

            return new
            {
                Pattern = "Proxy",
                Description = "Provides placeholder/surrogate to control access to another object",
                VirtualProxy = virtualProxyDemo,
                SecurityProxy = securityProxyDemo,
                CachingProxy = cachingProxyDemo,
                Example = "Image loading with lazy loading, access control, and caching",
                ProxyTypes = new[]
                {
                    "Virtual Proxy - Lazy loading of expensive objects",
                    "Protection Proxy - Access control and security",
                    "Caching Proxy - Result caching for performance"
                }
            };
        }

        // Complete Demo - All Patterns Together
        public object DemonstrateAllStructuralPatterns()
        {
            return new
            {
                Title = "Structural Design Patterns Demonstration",
                Description = "All structural patterns working together in various scenarios",
                Patterns = new
                {
                    Adapter = DemonstrateAdapter(),
                    Bridge = DemonstrateBridge(),
                    Composite = DemonstrateComposite(),
                    Decorator = DemonstrateDecorator(),
                    Facade = DemonstrateFacade(),
                    Flyweight = DemonstrateFlyweight(),
                    Proxy = DemonstrateProxy()
                },
                Summary = new
                {
                    TotalPatterns = 7,
                    Category = "Structural",
                    Purpose = "Deal with object composition and relationships between objects",
                    RealWorldApplications = new[]
                    {
                        "API integrations (Adapter)",
                        "Cross-platform development (Bridge)",
                        "UI component hierarchies (Composite)",
                        "Middleware pipelines (Decorator)",
                        "System APIs (Facade)",
                        "Game engines (Flyweight)",
                        "Remote services (Proxy)"
                    }
                }
            };
        }

        public object CreateCustomScenario(string patternType, Dictionary<string, object> parameters)
        {
            return patternType.ToLower() switch
            {
                "adapter" => new { Pattern = "Adapter", Message = "Custom adapter scenario", Parameters = parameters },
                "bridge" => new { Pattern = "Bridge", Message = "Custom bridge scenario", Parameters = parameters },
                "composite" => new { Pattern = "Composite", Message = "Custom composite scenario", Parameters = parameters },
                "decorator" => new { Pattern = "Decorator", Message = "Custom decorator scenario", Parameters = parameters },
                "facade" => new { Pattern = "Facade", Message = "Custom facade scenario", Parameters = parameters },
                "flyweight" => new { Pattern = "Flyweight", Message = "Custom flyweight scenario", Parameters = parameters },
                "proxy" => new { Pattern = "Proxy", Message = "Custom proxy scenario", Parameters = parameters },
                _ => new { Error = "Unknown pattern type", AvailablePatterns = new[] { "adapter", "bridge", "composite", "decorator", "facade", "flyweight", "proxy" } }
            };
        }

        public object GetStructuralPatternsInfo()
        {
            return new
            {
                Title = "Structural Design Patterns",
                Description = PatternConstants.STRUCTURAL_DESCRIPTION,
                Patterns = PatternConstants.STRUCTURAL_PATTERNS.Select(pattern => new
                {
                    Name = pattern,
                    Description = GetPatternDescription(pattern),
                    Endpoint = $"/api/StructuralPatterns/{pattern.ToLower().Replace(" ", "-")}"
                }),
                UseCases = new[]
                {
                    "Legacy system integration",
                    "Cross-platform development",
                    "UI component hierarchies",
                    "Feature enhancement",
                    "System API simplification",
                    "Memory optimization",
                    "Remote service access"
                }
            };
        }

        private string GetPatternDescription(string pattern)
        {
            return pattern switch
            {
                "Adapter" => "Allows incompatible interfaces to work together",
                "Bridge" => "Separates abstraction from implementation",
                "Composite" => "Composes objects into tree structures",
                "Decorator" => "Adds behavior to objects dynamically",
                "Facade" => "Provides simplified interface to complex subsystem",
                "Flyweight" => "Minimizes memory usage by sharing efficiently",
                "Proxy" => "Provides placeholder to control access to another object",
                _ => "Structural design pattern implementation"
            };
        }
    }
}