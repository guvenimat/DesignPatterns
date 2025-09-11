using DesignPatterns.Domain.Entities.Common;

namespace DesignPatterns.Domain.Interfaces.Structural
{
    // Adapter Pattern
    public interface IPaymentProcessor
    {
        bool ProcessPayment(decimal amount);
    }

    public interface ILegacyPaymentSystem
    {
        void MakePayment(double amount);
    }

    // Bridge Pattern
    public interface INotificationSender
    {
        void SendNotification(string message);
    }

    // Composite Pattern
    public interface IFileSystemItem
    {
        string Name { get; }
        int Size { get; }
        void Display(int depth = 0);
    }

    // Decorator Pattern
    public interface ICoffee
    {
        decimal Cost { get; }
        string Description { get; }
    }

    // Facade Pattern
    public interface IOrderFacade
    {
        bool PlaceOrder(User user, List<Product> products);
    }

    // Proxy Pattern
    public interface IImageLoader
    {
        void Display();
    }

    // Flyweight Pattern
    public interface ICharacterFlyweight
    {
        void Render(int x, int y, string color);
    }
}