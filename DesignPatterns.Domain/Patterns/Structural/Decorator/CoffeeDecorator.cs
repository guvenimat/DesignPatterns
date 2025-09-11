using DesignPatterns.Domain.Interfaces.Structural;

namespace DesignPatterns.Domain.Patterns.Structural.Decorator
{
    // Decorator Pattern - Add behavior to objects dynamically

    // Base Coffee Implementation
    public class SimpleCoffee : ICoffee
    {
        public decimal Cost => 2.00m;
        public string Description => "Simple Coffee";
    }

    // Base Decorator
    public abstract class CoffeeDecorator : ICoffee
    {
        protected ICoffee _coffee;

        protected CoffeeDecorator(ICoffee coffee)
        {
            _coffee = coffee;
        }

        public virtual decimal Cost => _coffee.Cost;
        public virtual string Description => _coffee.Description;
    }

    // Concrete Decorators
    public class MilkDecorator : CoffeeDecorator
    {
        public MilkDecorator(ICoffee coffee) : base(coffee) { }

        public override decimal Cost => _coffee.Cost + 0.50m;
        public override string Description => _coffee.Description + " + Milk";
    }

    public class SugarDecorator : CoffeeDecorator
    {
        public SugarDecorator(ICoffee coffee) : base(coffee) { }

        public override decimal Cost => _coffee.Cost + 0.25m;
        public override string Description => _coffee.Description + " + Sugar";
    }

    public class WhipCreamDecorator : CoffeeDecorator
    {
        public WhipCreamDecorator(ICoffee coffee) : base(coffee) { }

        public override decimal Cost => _coffee.Cost + 0.75m;
        public override string Description => _coffee.Description + " + Whip Cream";
    }

    public class VanillaDecorator : CoffeeDecorator
    {
        public VanillaDecorator(ICoffee coffee) : base(coffee) { }

        public override decimal Cost => _coffee.Cost + 0.60m;
        public override string Description => _coffee.Description + " + Vanilla";
    }

    public class CaramelDecorator : CoffeeDecorator
    {
        public CaramelDecorator(ICoffee coffee) : base(coffee) { }

        public override decimal Cost => _coffee.Cost + 0.65m;
        public override string Description => _coffee.Description + " + Caramel";
    }

    // Coffee Shop Service
    public class CoffeeShopService
    {
        public ICoffee CreateCustomCoffee(List<string> addOns)
        {
            ICoffee coffee = new SimpleCoffee();

            foreach (var addOn in addOns)
            {
                coffee = addOn.ToLower() switch
                {
                    "milk" => new MilkDecorator(coffee),
                    "sugar" => new SugarDecorator(coffee),
                    "whipcream" => new WhipCreamDecorator(coffee),
                    "vanilla" => new VanillaDecorator(coffee),
                    "caramel" => new CaramelDecorator(coffee),
                    _ => coffee
                };
            }

            return coffee;
        }

        public List<string> GetCoffeeMenu()
        {
            var menu = new List<string>();
            var addOnCombinations = new List<List<string>>
            {
                new() { },
                new() { "milk" },
                new() { "milk", "sugar" },
                new() { "milk", "vanilla" },
                new() { "whipcream", "caramel" },
                new() { "milk", "sugar", "vanilla", "whipcream" }
            };

            foreach (var combination in addOnCombinations)
            {
                var coffee = CreateCustomCoffee(combination);
                menu.Add($"{coffee.Description} - ${coffee.Cost:F2}");
            }

            return menu;
        }

        public string OrderCoffee(List<string> addOns)
        {
            var coffee = CreateCustomCoffee(addOns);
            return $"Ordered: {coffee.Description} for ${coffee.Cost:F2}";
        }
    }
}