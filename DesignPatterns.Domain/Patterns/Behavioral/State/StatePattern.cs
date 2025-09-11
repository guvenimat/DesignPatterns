using DesignPatterns.Domain.Entities.Common;
using DesignPatterns.Domain.Interfaces.Behavioral;

namespace DesignPatterns.Domain.Patterns.Behavioral.State
{
    // State Pattern - Allow object to change behavior when internal state changes

    // Concrete States
    public class PendingState : IOrderState
    {
        public void Process(Order order)
        {
            order.Status = "Processing";
            Console.WriteLine($"Order #{order.Id} moved from Pending to Processing");
        }

        public void Cancel(Order order)
        {
            order.Status = "Cancelled";
            Console.WriteLine($"Order #{order.Id} cancelled from Pending state");
        }

        public void Complete(Order order)
        {
            Console.WriteLine($"Cannot complete order #{order.Id} - must be processed first");
        }

        public string GetStateName() => "Pending";
    }

    public class ProcessingState : IOrderState
    {
        public void Process(Order order)
        {
            Console.WriteLine($"Order #{order.Id} is already being processed");
        }

        public void Cancel(Order order)
        {
            order.Status = "Cancelled";
            Console.WriteLine($"Order #{order.Id} cancelled during processing");
        }

        public void Complete(Order order)
        {
            order.Status = "Shipped";
            Console.WriteLine($"Order #{order.Id} processed and shipped");
        }

        public string GetStateName() => "Processing";
    }

    public class ShippedState : IOrderState
    {
        public void Process(Order order)
        {
            Console.WriteLine($"Order #{order.Id} has already been shipped");
        }

        public void Cancel(Order order)
        {
            Console.WriteLine($"Cannot cancel order #{order.Id} - already shipped");
        }

        public void Complete(Order order)
        {
            order.Status = "Delivered";
            Console.WriteLine($"Order #{order.Id} delivered successfully");
        }

        public string GetStateName() => "Shipped";
    }

    public class DeliveredState : IOrderState
    {
        public void Process(Order order)
        {
            Console.WriteLine($"Order #{order.Id} has already been delivered");
        }

        public void Cancel(Order order)
        {
            Console.WriteLine($"Cannot cancel order #{order.Id} - already delivered");
        }

        public void Complete(Order order)
        {
            Console.WriteLine($"Order #{order.Id} is already completed");
        }

        public string GetStateName() => "Delivered";
    }

    public class CancelledState : IOrderState
    {
        public void Process(Order order)
        {
            Console.WriteLine($"Cannot process cancelled order #{order.Id}");
        }

        public void Cancel(Order order)
        {
            Console.WriteLine($"Order #{order.Id} is already cancelled");
        }

        public void Complete(Order order)
        {
            Console.WriteLine($"Cannot complete cancelled order #{order.Id}");
        }

        public string GetStateName() => "Cancelled";
    }

    // Context class that uses states
    public class OrderStateMachine
    {
        private readonly Dictionary<string, IOrderState> _states;
        private IOrderState _currentState;

        public OrderStateMachine()
        {
            _states = new Dictionary<string, IOrderState>
            {
                {"Pending", new PendingState()},
                {"Processing", new ProcessingState()},
                {"Shipped", new ShippedState()},
                {"Delivered", new DeliveredState()},
                {"Cancelled", new CancelledState()}
            };
            _currentState = _states["Pending"];
        }

        public void SetState(string stateName)
        {
            if (_states.TryGetValue(stateName, out var state))
            {
                _currentState = state;
            }
        }

        public void ProcessOrder(Order order)
        {
            _currentState.Process(order);
            UpdateState(order);
        }

        public void CancelOrder(Order order)
        {
            _currentState.Cancel(order);
            UpdateState(order);
        }

        public void CompleteOrder(Order order)
        {
            _currentState.Complete(order);
            UpdateState(order);
        }

        public string GetCurrentStateName()
        {
            return _currentState.GetStateName();
        }

        private void UpdateState(Order order)
        {
            if (_states.TryGetValue(order.Status, out var newState))
            {
                _currentState = newState;
            }
        }
    }

    // Traffic Light State Machine Example
    public interface ITrafficLightState
    {
        void Handle(TrafficLight context);
        string GetColor();
        int GetDuration();
    }

    public class RedLightState : ITrafficLightState
    {
        public void Handle(TrafficLight context)
        {
            Console.WriteLine("Red light -> Changing to Green");
            context.SetState(new GreenLightState());
        }

        public string GetColor() => "Red";
        public int GetDuration() => 30; // seconds
    }

    public class GreenLightState : ITrafficLightState
    {
        public void Handle(TrafficLight context)
        {
            Console.WriteLine("Green light -> Changing to Yellow");
            context.SetState(new YellowLightState());
        }

        public string GetColor() => "Green";
        public int GetDuration() => 25; // seconds
    }

    public class YellowLightState : ITrafficLightState
    {
        public void Handle(TrafficLight context)
        {
            Console.WriteLine("Yellow light -> Changing to Red");
            context.SetState(new RedLightState());
        }

        public string GetColor() => "Yellow";
        public int GetDuration() => 5; // seconds
    }

    public class TrafficLight
    {
        private ITrafficLightState _state;

        public TrafficLight()
        {
            _state = new RedLightState();
        }

        public void SetState(ITrafficLightState state)
        {
            _state = state;
        }

        public void Change()
        {
            _state.Handle(this);
        }

        public string GetCurrentColor()
        {
            return _state.GetColor();
        }

        public int GetCurrentDuration()
        {
            return _state.GetDuration();
        }

        public string GetStatus()
        {
            return $"Traffic Light: {GetCurrentColor()} ({GetCurrentDuration()}s)";
        }
    }

    // State Service
    public class StateService
    {
        public List<string> DemonstrateOrderStateMachine()
        {
            var results = new List<string>();
            var order = new Order
            {
                Id = 12345,
                UserId = 1,
                Products = new List<Product>
                {
                    new() { Id = 1, Name = "Laptop", Price = 1000m },
                    new() { Id = 2, Name = "Mouse", Price = 25m }
                },
                TotalAmount = 1025m,
                OrderDate = DateTime.UtcNow,
                Status = "Pending"
            };

            var stateMachine = new OrderStateMachine();

            using var stringWriter = new StringWriter();
            var originalOut = Console.Out;
            Console.SetOut(stringWriter);

            Console.WriteLine($"=== Order State Machine Demo ===");
            Console.WriteLine($"Initial order: {order}");
            Console.WriteLine($"Current state: {stateMachine.GetCurrentStateName()}");
            Console.WriteLine();

            // Process the order through different states
            stateMachine.ProcessOrder(order);
            Console.WriteLine($"Order status: {order.Status}, State: {stateMachine.GetCurrentStateName()}");

            stateMachine.CompleteOrder(order);
            Console.WriteLine($"Order status: {order.Status}, State: {stateMachine.GetCurrentStateName()}");

            stateMachine.CompleteOrder(order);
            Console.WriteLine($"Order status: {order.Status}, State: {stateMachine.GetCurrentStateName()}");

            Console.WriteLine("\n=== Testing Cancellation ===");
            
            // Create another order to test cancellation
            var order2 = new Order
            {
                Id = 12346,
                Status = "Pending"
            };
            
            var stateMachine2 = new OrderStateMachine();
            stateMachine2.CancelOrder(order2);
            stateMachine2.SetState(order2.Status);
            Console.WriteLine($"Order 2 status: {order2.Status}");
            
            // Try to process cancelled order
            stateMachine2.ProcessOrder(order2);

            Console.SetOut(originalOut);
            results.Add(stringWriter.ToString());

            return results;
        }

        public List<string> DemonstrateTrafficLightStateMachine()
        {
            var results = new List<string>();
            var trafficLight = new TrafficLight();

            using var stringWriter = new StringWriter();
            var originalOut = Console.Out;
            Console.SetOut(stringWriter);

            Console.WriteLine("=== Traffic Light State Machine Demo ===");
            
            for (int i = 0; i < 6; i++)
            {
                Console.WriteLine($"Cycle {i + 1}: {trafficLight.GetStatus()}");
                trafficLight.Change();
            }

            Console.SetOut(originalOut);
            results.Add(stringWriter.ToString());

            return results;
        }

        public List<string> CreateCustomOrderScenario(int orderId, List<string> actions)
        {
            var results = new List<string>();
            var order = new Order
            {
                Id = orderId,
                UserId = 1,
                TotalAmount = 500m,
                OrderDate = DateTime.UtcNow,
                Status = "Pending"
            };

            var stateMachine = new OrderStateMachine();

            using var stringWriter = new StringWriter();
            var originalOut = Console.Out;
            Console.SetOut(stringWriter);

            Console.WriteLine($"=== Custom Order Scenario #{orderId} ===");
            Console.WriteLine($"Initial state: {stateMachine.GetCurrentStateName()}");

            foreach (var action in actions)
            {
                Console.WriteLine($"\nAction: {action}");
                switch (action.ToLower())
                {
                    case "process":
                        stateMachine.ProcessOrder(order);
                        break;
                    case "cancel":
                        stateMachine.CancelOrder(order);
                        break;
                    case "complete":
                        stateMachine.CompleteOrder(order);
                        break;
                    default:
                        Console.WriteLine($"Unknown action: {action}");
                        break;
                }
                Console.WriteLine($"Current state: {stateMachine.GetCurrentStateName()}");
            }

            Console.SetOut(originalOut);
            results.Add(stringWriter.ToString());

            return results;
        }
    }
}