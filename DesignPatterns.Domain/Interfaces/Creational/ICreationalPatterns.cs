using DesignPatterns.Domain.Entities.Common;

namespace DesignPatterns.Domain.Interfaces.Creational
{
    // Factory Pattern Interfaces
    public interface IProductFactory
    {
        Product CreateProduct(string type);
    }

    public interface IAbstractFactory
    {
        Product CreateProduct();
        User CreateUser();
    }

    // Builder Pattern Interface
    public interface IProductBuilder
    {
        IProductBuilder SetName(string name);
        IProductBuilder SetPrice(decimal price);
        IProductBuilder SetDescription(string description);
        IProductBuilder SetCategory(string category);
        Product Build();
    }

    // Singleton Pattern Interface
    public interface IConfigurationManager
    {
        string GetConfiguration(string key);
        void SetConfiguration(string key, string value);
    }
}