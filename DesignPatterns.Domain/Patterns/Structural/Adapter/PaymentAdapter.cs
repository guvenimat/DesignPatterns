using DesignPatterns.Domain.Interfaces.Structural;

namespace DesignPatterns.Domain.Patterns.Structural.Adapter
{
    // Legacy Payment System (Third-party)
    public class LegacyPaymentSystem : ILegacyPaymentSystem
    {
        public void MakePayment(double amount)
        {
            Console.WriteLine($"Legacy payment processed: ${amount:F2}");
        }
    }

    // Adapter Pattern - Adapts legacy system to modern interface
    public class PaymentAdapter : IPaymentProcessor
    {
        private readonly ILegacyPaymentSystem _legacySystem;

        public PaymentAdapter(ILegacyPaymentSystem legacySystem)
        {
            _legacySystem = legacySystem;
        }

        public bool ProcessPayment(decimal amount)
        {
            try
            {
                // Convert decimal to double and adapt the interface
                _legacySystem.MakePayment((double)amount);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    // Modern Payment Processor
    public class ModernPaymentProcessor : IPaymentProcessor
    {
        public bool ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Modern payment processed: ${amount:F2}");
            return amount > 0;
        }
    }

    // Payment Service that uses different processors
    public class PaymentService
    {
        private readonly List<IPaymentProcessor> _processors;

        public PaymentService()
        {
            _processors = new List<IPaymentProcessor>
            {
                new ModernPaymentProcessor(),
                new PaymentAdapter(new LegacyPaymentSystem())
            };
        }

        public List<string> ProcessPaymentWithAllSystems(decimal amount)
        {
            var results = new List<string>();
            
            foreach (var processor in _processors)
            {
                var success = processor.ProcessPayment(amount);
                var processorType = processor.GetType().Name;
                results.Add($"{processorType}: {(success ? "Success" : "Failed")}");
            }

            return results;
        }
    }
}