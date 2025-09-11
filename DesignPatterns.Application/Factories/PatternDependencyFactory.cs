using DesignPatterns.Domain.Patterns.Creational.Singleton;
using DesignPatterns.Domain.Patterns.Creational.Factory;
using DesignPatterns.Domain.Patterns.Creational.AbstractFactory;
using DesignPatterns.Domain.Patterns.Creational.Builder;
using DesignPatterns.Domain.Patterns.Creational.Prototype;
using DesignPatterns.Domain.Interfaces.Creational;

namespace DesignPatterns.Application.Factories;

public interface IPatternDependencyFactory
{
    IProductFactory CreateProductFactory();
    IProductBuilder CreateProductBuilder();
    PrototypeRegistry CreatePrototypeRegistry();
}

public class PatternDependencyFactory : IPatternDependencyFactory
{
    public IProductFactory CreateProductFactory()
    {
        return new ProductFactory();
    }

    public IProductBuilder CreateProductBuilder()
    {
        return new ProductBuilder();
    }

    public PrototypeRegistry CreatePrototypeRegistry()
    {
        return new PrototypeRegistry();
    }
}