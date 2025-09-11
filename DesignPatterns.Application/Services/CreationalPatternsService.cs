using DesignPatterns.Domain.Patterns.Creational.Singleton;
using DesignPatterns.Domain.Patterns.Creational.Factory;
using DesignPatterns.Domain.Patterns.Creational.AbstractFactory;
using DesignPatterns.Domain.Patterns.Creational.Builder;
using DesignPatterns.Domain.Patterns.Creational.Prototype;
using DesignPatterns.Application.Interfaces;
using DesignPatterns.Application.Factories;
using DesignPatterns.Domain.Interfaces.Creational;
using DesignPatterns.Domain.Entities.Common;
using DesignPatterns.Domain.Constants;

namespace DesignPatterns.Application.Services
{
    public class CreationalPatternsService : ICreationalPatternsService
    {
        private readonly IProductFactory _productFactory;
        private readonly IProductBuilder _productBuilder;
        private readonly PrototypeRegistry _prototypeRegistry;

        public CreationalPatternsService(IPatternDependencyFactory dependencyFactory)
        {
            _productFactory = dependencyFactory.CreateProductFactory();
            _productBuilder = dependencyFactory.CreateProductBuilder();
            _prototypeRegistry = dependencyFactory.CreatePrototypeRegistry();
        }

        // Singleton Pattern Demo
        public object DemonstrateSingleton()
        {
            var config = ConfigurationManager.Instance;
            var initialConfigs = config.GetAllConfigurations();

            config.SetConfiguration("TestKey", "TestValue");
            config.SetConfiguration("Environment", "Production");

            // Get another instance to prove it's the same
            var config2 = ConfigurationManager.Instance;
            var finalConfigs = config2.GetAllConfigurations();

            return new
            {
                Pattern = "Singleton",
                Description = "Ensures a class has only one instance and provides global access",
                InitialConfigurations = initialConfigs,
                FinalConfigurations = finalConfigs,
                AreSameInstance = ReferenceEquals(config, config2),
                Example = "Configuration Manager - only one instance manages all app settings"
            };
        }

        // Factory Pattern Demo
        public object DemonstrateFactory()
        {
            var products = new List<object>();
            var types = new[] { "electronics", "clothing", "books" };

            foreach (var type in types)
            {
                try
                {
                    var product = _productFactory.CreateProduct(type);
                    products.Add(new
                    {
                        Type = type,
                        Product = product,
                        Success = true
                    });
                }
                catch (Exception ex)
                {
                    products.Add(new
                    {
                        Type = type,
                        Error = ex.Message,
                        Success = false
                    });
                }
            }

            // Factory Method Pattern
            var creators = new List<(string Type, ProductCreator Creator)>
            {
                ("Electronics", new ElectronicsCreator()),
                ("Clothing", new ClothingCreator())
            };

            var factoryMethodResults = creators.Select(c => new
            {
                c.Type,
                Product = c.Creator.CreateProduct(),
                ProcessResult = c.Creator.ProcessProduct()
            }).ToList();

            return new
            {
                Pattern = "Factory",
                Description = "Creates objects without specifying exact classes",
                SimpleFactory = products,
                FactoryMethod = factoryMethodResults,
                Example = "Product creation based on type - electronics, clothing, books"
            };
        }

        // Abstract Factory Pattern Demo
        public object DemonstrateAbstractFactory()
        {
            var storeTypes = new[] { "online", "physical" };
            var results = new List<object>();

            foreach (var storeType in storeTypes)
            {
                try
                {
                    var factory = StoreFactoryProvider.GetFactory(storeType);
                    var product = factory.CreateProduct();
                    var user = factory.CreateUser();

                    results.Add(new
                    {
                        StoreType = storeType,
                        Product = product,
                        User = user,
                        Success = true
                    });
                }
                catch (Exception ex)
                {
                    results.Add(new
                    {
                        StoreType = storeType,
                        Error = ex.Message,
                        Success = false
                    });
                }
            }

            return new
            {
                Pattern = "Abstract Factory",
                Description = "Creates families of related objects",
                Results = results,
                Example = "Different store types create different product and user combinations"
            };
        }

        // Builder Pattern Demo
        public object DemonstrateBuilder()
        {
            var director = new ProductDirector(_productBuilder);

            var products = new List<object>
            {
                new { Type = "Laptop", Product = director.BuildLaptop() },
                new { Type = "Book", Product = director.BuildBook() },
                new { Type = "Clothing", Product = director.BuildClothing() }
            };

            // Custom building
            var customProduct = _productBuilder
                .SetName("Custom Gaming Setup")
                .SetPrice(2499.99m)
                .SetDescription("Complete gaming setup with RGB lighting")
                .SetCategory("Gaming")
                .Build();

            return new
            {
                Pattern = "Builder",
                Description = "Constructs complex objects step by step",
                DirectorBuiltProducts = products,
                CustomProduct = customProduct,
                Example = "Building different product types with varying complexity"
            };
        }

        // Prototype Pattern Demo
        public object DemonstratePrototype()
        {
            var prototypes = new List<object>();
            var types = new[] { "electronics", "clothing" };

            foreach (var type in types)
            {
                try
                {
                    var prototype = _prototypeRegistry.GetPrototype(type);
                    
                    // Customize the prototype
                    prototype.Id = new Random().Next(1000, 9999);
                    prototype.Name = $"Custom {type} Product";
                    prototype.Price = new Random().Next(100, 1000);

                    // Clone the customized prototype
                    var clone = prototype.Clone();
                    clone.Id = new Random().Next(1000, 9999);
                    clone.Name += " (Clone)";

                    prototypes.Add(new
                    {
                        Type = type,
                        Original = prototype,
                        Clone = clone,
                        AreDifferentInstances = !ReferenceEquals(prototype, clone),
                        HaveSameBaseProperties = prototype.Category == clone.Category
                    });
                }
                catch (Exception ex)
                {
                    prototypes.Add(new
                    {
                        Type = type,
                        Error = ex.Message
                    });
                }
            }

            return new
            {
                Pattern = "Prototype",
                Description = "Creates objects by cloning existing instances",
                Results = prototypes,
                Example = "Cloning product prototypes to create new instances with shared characteristics"
            };
        }

        // Complete Demo - All Patterns Together
        public object DemonstrateAllCreationalPatterns()
        {
            return new
            {
                Title = "Creational Design Patterns Demonstration",
                Description = "All creational patterns working together in an e-commerce scenario",
                Patterns = new
                {
                    Singleton = DemonstrateSingleton(),
                    Factory = DemonstrateFactory(),
                    AbstractFactory = DemonstrateAbstractFactory(),
                    Builder = DemonstrateBuilder(),
                    Prototype = DemonstratePrototype()
                },
                Summary = new
                {
                    TotalPatterns = 5,
                    Category = "Creational",
                    Purpose = "Object creation mechanisms that increase flexibility and reuse",
                    RealWorldApplications = new[]
                    {
                        "Database connection pools (Singleton)",
                        "UI component factories (Factory)",
                        "Cross-platform GUI frameworks (Abstract Factory)",
                        "SQL query builders (Builder)",
                        "Game object spawning (Prototype)"
                    }
                }
            };
        }

        public object CreateCustomProduct(string name, decimal price, string description, string category)
        {
            var product = _productBuilder
                .SetName(name)
                .SetPrice(price)
                .SetDescription(description)
                .SetCategory(category)
                .Build();

            return new
            {
                Message = "Custom product created using Builder pattern",
                Product = product,
                BuilderUsed = nameof(ProductBuilder)
            };
        }

        public object GetCreationalPatternsInfo()
        {
            return new
            {
                Title = "Creational Design Patterns",
                Description = PatternConstants.CREATIONAL_DESCRIPTION,
                Patterns = PatternConstants.CREATIONAL_PATTERNS.Select(pattern => new
                {
                    Name = pattern,
                    Description = GetPatternDescription(pattern),
                    Endpoint = $"/api/CreationalPatterns/{pattern.ToLower().Replace(" ", "-")}"
                }),
                UseCases = new[]
                {
                    "Database connection management",
                    "UI component creation",
                    "Cross-platform object families",
                    "Complex configuration objects",
                    "Game object spawning"
                }
            };
        }

        private string GetPatternDescription(string pattern)
        {
            return pattern switch
            {
                "Singleton" => "Ensures a class has only one instance",
                "Factory" => "Creates objects without specifying exact classes",
                "Abstract Factory" => "Creates families of related objects",
                "Builder" => "Constructs complex objects step by step",
                "Prototype" => "Creates objects by cloning existing instances",
                _ => "Design pattern implementation"
            };
        }
    }
}