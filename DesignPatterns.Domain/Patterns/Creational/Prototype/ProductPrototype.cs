using DesignPatterns.Domain.Entities.Common;
using DesignPatterns.Domain.Interfaces.Common;

namespace DesignPatterns.Domain.Patterns.Creational.Prototype
{
    // Prototype Pattern
    public class ProductPrototype : IPrototype<ProductPrototype>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public List<string> Features { get; set; } = new();
        public ProductSpecifications Specifications { get; set; } = new();

        public ProductPrototype Clone()
        {
            var clone = new ProductPrototype
            {
                Id = this.Id,
                Name = this.Name,
                Price = this.Price,
                Description = this.Description,
                Category = this.Category,
                Features = new List<string>(this.Features),
                Specifications = this.Specifications.Clone()
            };
            return clone;
        }

        public Product ToProduct()
        {
            return new Product
            {
                Id = this.Id,
                Name = this.Name,
                Price = this.Price,
                Description = this.Description,
                Category = this.Category
            };
        }
    }

    public class ProductSpecifications : IPrototype<ProductSpecifications>
    {
        public string Color { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public double Weight { get; set; }
        public Dictionary<string, string> TechnicalSpecs { get; set; } = new();

        public ProductSpecifications Clone()
        {
            return new ProductSpecifications
            {
                Color = this.Color,
                Size = this.Size,
                Weight = this.Weight,
                TechnicalSpecs = new Dictionary<string, string>(this.TechnicalSpecs)
            };
        }
    }

    public class PrototypeRegistry
    {
        private readonly Dictionary<string, ProductPrototype> _prototypes = new();

        public PrototypeRegistry()
        {
            InitializePrototypes();
        }

        private void InitializePrototypes()
        {
            // Electronics prototype
            var electronicsPrototype = new ProductPrototype
            {
                Id = 0,
                Name = "Electronic Device",
                Price = 0,
                Description = "Base electronic device",
                Category = "Electronics",
                Features = new List<string> { "Digital Display", "USB Port" },
                Specifications = new ProductSpecifications
                {
                    Color = "Black",
                    Size = "Medium",
                    Weight = 1.0,
                    TechnicalSpecs = new Dictionary<string, string>
                    {
                        {"Warranty", "1 Year"},
                        {"Power", "AC/DC"}
                    }
                }
            };

            // Clothing prototype
            var clothingPrototype = new ProductPrototype
            {
                Id = 0,
                Name = "Clothing Item",
                Price = 0,
                Description = "Base clothing item",
                Category = "Clothing",
                Features = new List<string> { "Machine Washable", "Cotton Blend" },
                Specifications = new ProductSpecifications
                {
                    Color = "Blue",
                    Size = "M",
                    Weight = 0.5,
                    TechnicalSpecs = new Dictionary<string, string>
                    {
                        {"Material", "Cotton"},
                        {"Care", "Machine Wash"}
                    }
                }
            };

            _prototypes["electronics"] = electronicsPrototype;
            _prototypes["clothing"] = clothingPrototype;
        }

        public ProductPrototype GetPrototype(string type)
        {
            if (_prototypes.TryGetValue(type.ToLower(), out var prototype))
            {
                return prototype.Clone();
            }
            throw new ArgumentException($"Prototype not found for type: {type}");
        }

        public void RegisterPrototype(string type, ProductPrototype prototype)
        {
            _prototypes[type.ToLower()] = prototype;
        }
    }
}