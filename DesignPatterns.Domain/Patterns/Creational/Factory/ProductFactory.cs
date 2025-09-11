using DesignPatterns.Domain.Entities.Common;
using DesignPatterns.Domain.Interfaces.Creational;

namespace DesignPatterns.Domain.Patterns.Creational.Factory
{
    // Simple Factory Pattern
    public class ProductFactory : IProductFactory
    {
        public Product CreateProduct(string type)
        {
            return type.ToLower() switch
            {
                "electronics" => new Product
                {
                    Id = 1,
                    Name = "Smartphone",
                    Price = 699.99m,
                    Description = "Latest smartphone with advanced features",
                    Category = "Electronics"
                },
                "clothing" => new Product
                {
                    Id = 2,
                    Name = "T-Shirt",
                    Price = 29.99m,
                    Description = "Comfortable cotton t-shirt",
                    Category = "Clothing"
                },
                "books" => new Product
                {
                    Id = 3,
                    Name = "Design Patterns Book",
                    Price = 49.99m,
                    Description = "Comprehensive guide to design patterns",
                    Category = "Books"
                },
                _ => throw new ArgumentException($"Unknown product type: {type}")
            };
        }
    }

    // Factory Method Pattern
    public abstract class ProductCreator
    {
        public abstract Product CreateProduct();
        
        public string ProcessProduct()
        {
            var product = CreateProduct();
            return $"Processing {product.Name} in {product.Category} category";
        }
    }

    public class ElectronicsCreator : ProductCreator
    {
        public override Product CreateProduct()
        {
            return new Product
            {
                Id = 10,
                Name = "Laptop",
                Price = 1299.99m,
                Description = "High-performance laptop for professionals",
                Category = "Electronics"
            };
        }
    }

    public class ClothingCreator : ProductCreator
    {
        public override Product CreateProduct()
        {
            return new Product
            {
                Id = 11,
                Name = "Jeans",
                Price = 79.99m,
                Description = "Premium denim jeans",
                Category = "Clothing"
            };
        }
    }
}