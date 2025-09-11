using DesignPatterns.Domain.Entities.Common;
using DesignPatterns.Domain.Interfaces.Creational;

namespace DesignPatterns.Domain.Patterns.Creational.AbstractFactory
{
    // Abstract Factory Pattern
    public class OnlineStoreFactory : IAbstractFactory
    {
        public Product CreateProduct()
        {
            return new Product
            {
                Id = 100,
                Name = "Online Product",
                Price = 199.99m,
                Description = "Product optimized for online sales",
                Category = "Online"
            };
        }

        public User CreateUser()
        {
            return new User
            {
                Id = 100,
                Username = "online_user",
                Email = "user@online.com",
                Role = "Customer",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };
        }
    }

    public class PhysicalStoreFactory : IAbstractFactory
    {
        public Product CreateProduct()
        {
            return new Product
            {
                Id = 200,
                Name = "Physical Product",
                Price = 299.99m,
                Description = "Product available in physical stores",
                Category = "Physical"
            };
        }

        public User CreateUser()
        {
            return new User
            {
                Id = 200,
                Username = "store_customer",
                Email = "customer@store.com",
                Role = "StoreCustomer",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };
        }
    }

    public class StoreFactoryProvider
    {
        public static IAbstractFactory GetFactory(string storeType)
        {
            return storeType.ToLower() switch
            {
                "online" => new OnlineStoreFactory(),
                "physical" => new PhysicalStoreFactory(),
                _ => throw new ArgumentException($"Unknown store type: {storeType}")
            };
        }
    }
}