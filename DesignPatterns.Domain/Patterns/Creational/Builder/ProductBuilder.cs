using DesignPatterns.Domain.Entities.Common;
using DesignPatterns.Domain.Interfaces.Creational;

namespace DesignPatterns.Domain.Patterns.Creational.Builder
{
    // Builder Pattern
    public class ProductBuilder : IProductBuilder
    {
        private Product _product = new Product();

        public ProductBuilder()
        {
            Reset();
        }

        public void Reset()
        {
            _product = new Product();
        }

        public IProductBuilder SetName(string name)
        {
            _product.Name = name;
            return this;
        }

        public IProductBuilder SetPrice(decimal price)
        {
            _product.Price = price;
            return this;
        }

        public IProductBuilder SetDescription(string description)
        {
            _product.Description = description;
            return this;
        }

        public IProductBuilder SetCategory(string category)
        {
            _product.Category = category;
            return this;
        }

        public Product Build()
        {
            var result = _product;
            Reset();
            return result;
        }
    }

    // Director class to construct specific products
    public class ProductDirector
    {
        private IProductBuilder _builder;

        public ProductDirector(IProductBuilder builder)
        {
            _builder = builder;
        }

        public Product BuildLaptop()
        {
            return _builder
                .SetName("Gaming Laptop")
                .SetPrice(1499.99m)
                .SetDescription("High-performance gaming laptop with RTX graphics")
                .SetCategory("Electronics")
                .Build();
        }

        public Product BuildBook()
        {
            return _builder
                .SetName("Clean Code")
                .SetPrice(39.99m)
                .SetDescription("A handbook of agile software craftsmanship")
                .SetCategory("Books")
                .Build();
        }

        public Product BuildClothing()
        {
            return _builder
                .SetName("Designer Jacket")
                .SetPrice(199.99m)
                .SetDescription("Premium designer jacket for all seasons")
                .SetCategory("Clothing")
                .Build();
        }
    }
}