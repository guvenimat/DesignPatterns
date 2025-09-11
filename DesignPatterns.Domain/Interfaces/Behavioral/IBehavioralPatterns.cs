using DesignPatterns.Domain.Entities.Common;
using DesignPatterns.Domain.Interfaces.Common;

namespace DesignPatterns.Domain.Interfaces.Behavioral
{
    // State Pattern
    public interface IOrderState
    {
        void Process(Order order);
        void Cancel(Order order);
        void Complete(Order order);
        string GetStateName();
    }

    // Template Method Pattern
    public abstract class OrderProcessingTemplate
    {
        public string ProcessOrder(Order order)
        {
            var result = "Order Processing Steps:\n";
            result += ValidateOrder(order) + "\n";
            result += CalculateTotal(order) + "\n";
            result += ProcessPayment(order) + "\n";
            result += SendConfirmation(order) + "\n";
            return result;
        }

        protected abstract string ValidateOrder(Order order);
        protected abstract string CalculateTotal(Order order);
        protected abstract string ProcessPayment(Order order);
        protected virtual string SendConfirmation(Order order)
        {
            return "Confirmation email sent";
        }
    }

    // Strategy Pattern for Discount Calculation
    public interface IDiscountStrategy
    {
        decimal ApplyDiscount(decimal amount);
        string GetDiscountDescription();
    }

    // Observer Pattern for News System
    public interface INewsSubscriber
    {
        void Update(string news);
        string GetSubscriberName();
    }

    public interface INewsPublisher
    {
        void Subscribe(INewsSubscriber subscriber);
        void Unsubscribe(INewsSubscriber subscriber);
        void PublishNews(string news);
    }
}