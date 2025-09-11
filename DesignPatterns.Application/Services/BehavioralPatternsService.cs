using DesignPatterns.Domain.Patterns.Behavioral.Observer;
using DesignPatterns.Domain.Patterns.Behavioral.Strategy;
using DesignPatterns.Domain.Patterns.Behavioral.Command;
using DesignPatterns.Domain.Patterns.Behavioral.State;
using DesignPatterns.Domain.Patterns.Behavioral.TemplateMethod;
using DesignPatterns.Domain.Patterns.Behavioral.ChainOfResponsibility;
using DesignPatterns.Domain.Patterns.Behavioral.Iterator;
using DesignPatterns.Domain.Patterns.Behavioral.Mediator;
using DesignPatterns.Domain.Patterns.Behavioral.Memento;
using DesignPatterns.Domain.Patterns.Behavioral.Visitor;
using DesignPatterns.Application.Interfaces;
using DesignPatterns.Domain.Constants;

namespace DesignPatterns.Application.Services
{
    public class BehavioralPatternsService : IBehavioralPatternsService
    {
        private readonly ObserverService _observerService;
        private readonly StrategyService _strategyService;
        private readonly CommandService _commandService;
        private readonly StateService _stateService;
        private readonly TemplateMethodService _templateMethodService;
        private readonly ChainOfResponsibilityService _chainService;
        private readonly IteratorService _iteratorService;
        private readonly MediatorService _mediatorService;
        private readonly MementoService _mementoService;
        private readonly VisitorService _visitorService;

        public BehavioralPatternsService()
        {
            _observerService = new ObserverService();
            _strategyService = new StrategyService();
            _commandService = new CommandService();
            _stateService = new StateService();
            _templateMethodService = new TemplateMethodService();
            _chainService = new ChainOfResponsibilityService();
            _iteratorService = new IteratorService();
            _mediatorService = new MediatorService();
            _mementoService = new MementoService();
            _visitorService = new VisitorService();
        }

        // Observer Pattern Demo
        public object DemonstrateObserver()
        {
            var newsDemo = _observerService.DemonstrateNewsObserver();
            var stockDemo = _observerService.DemonstrateStockObserver();

            return new
            {
                Pattern = "Observer",
                Description = "Defines one-to-many dependency between objects",
                NewsSystemDemo = newsDemo,
                StockSystemDemo = stockDemo,
                Example = "News subscription system and stock price monitoring",
                Benefits = new[]
                {
                    "Loose coupling between subject and observers",
                    "Dynamic subscription/unsubscription",
                    "Broadcast communication"
                }
            };
        }

        // Strategy Pattern Demo
        public object DemonstrateStrategy()
        {
            var discountDemo = _strategyService.DemonstrateDiscountStrategies();
            var paymentDemo = _strategyService.DemonstratePaymentStrategies();
            var customScenario = _strategyService.CreateCustomScenario(
                new List<(string, decimal, int)>
                {
                    ("Gaming Laptop", 1500.00m, 1),
                    ("Gaming Mouse", 75.00m, 1),
                    ("Mechanical Keyboard", 120.00m, 1)
                },
                "vip"
            );

            return new
            {
                Pattern = "Strategy",
                Description = "Defines family of algorithms and makes them interchangeable",
                DiscountStrategies = discountDemo,
                PaymentStrategies = paymentDemo,
                CustomScenario = customScenario,
                Example = "Different discount strategies and payment methods",
                Benefits = new[]
                {
                    "Algorithm can vary independently from clients",
                    "Eliminates conditional statements",
                    "Runtime algorithm selection"
                }
            };
        }

        // Command Pattern Demo
        public object DemonstrateCommand()
        {
            var textEditorDemo = _commandService.DemonstrateTextEditorCommands();
            var macroDemo = _commandService.DemonstrateMacroCommand();
            var remoteControlDemo = _commandService.DemonstrateRemoteControl();

            return new
            {
                Pattern = "Command",
                Description = "Encapsulates requests as objects",
                TextEditorDemo = textEditorDemo,
                MacroCommandDemo = macroDemo,
                RemoteControlDemo = remoteControlDemo,
                Example = "Text editor with undo/redo and smart home remote control",
                Benefits = new[]
                {
                    "Decouples invoker from receiver",
                    "Supports undo/redo operations",
                    "Enables macro commands and queuing"
                }
            };
        }

        // State Pattern Demo
        public object DemonstrateState()
        {
            var orderStateMachine = _stateService.DemonstrateOrderStateMachine();
            var trafficLightDemo = _stateService.DemonstrateTrafficLightStateMachine();
            var customScenario = _stateService.CreateCustomOrderScenario(
                12345,
                new List<string> { "process", "complete", "process" }
            );

            return new
            {
                Pattern = "State",
                Description = "Allows object to change behavior when internal state changes",
                OrderStateMachine = orderStateMachine,
                TrafficLightDemo = trafficLightDemo,
                CustomScenario = customScenario,
                Example = "Order processing states and traffic light control",
                Benefits = new[]
                {
                    "Localizes state-specific behavior",
                    "Makes state transitions explicit",
                    "State objects can be shared"
                }
            };
        }

        // Template Method Pattern Demo
        public object DemonstrateTemplateMethod()
        {
            var orderProcessingDemo = _templateMethodService.DemonstrateOrderProcessing();
            var dataMiningDemo = _templateMethodService.DemonstrateDataMining();
            var customOrder = _templateMethodService.ProcessCustomOrder(
                "wholesale",
                new List<(string, decimal)>
                {
                    ("Bulk Electronics", 5000.00m),
                    ("Wholesale Accessories", 1200.00m)
                }
            );

            return new
            {
                Pattern = "Template Method",
                Description = "Defines skeleton of algorithm, lets subclasses override specific steps",
                OrderProcessingDemo = orderProcessingDemo,
                DataMiningDemo = dataMiningDemo,
                CustomOrderDemo = customOrder,
                Example = "Different order processing types and data mining algorithms",
                Benefits = new[]
                {
                    "Code reuse through inheritance",
                    "Control over algorithm structure",
                    "Hook methods for customization"
                }
            };
        }

        // Chain of Responsibility Pattern Demo
        public object DemonstrateChainOfResponsibility()
        {
            var supportChainDemo = _chainService.DemonstrateSupportChain();
            var expenseApprovalDemo = _chainService.DemonstrateExpenseApprovalChain();
            var customSupport = _chainService.ProcessCustomSupportRequest(
                "technical", 4, "Database connection issues affecting multiple users"
            );
            var customExpense = _chainService.ProcessCustomExpenseRequest(
                7500.00m, "New server equipment", "IT Department"
            );

            return new
            {
                Pattern = "Chain of Responsibility",
                Description = "Passes requests along chain of handlers",
                SupportChainDemo = supportChainDemo,
                ExpenseApprovalDemo = expenseApprovalDemo,
                CustomSupportRequest = customSupport,
                CustomExpenseRequest = customExpense,
                Example = "Support ticket routing and expense approval workflow",
                Benefits = new[]
                {
                    "Decouples sender from receiver",
                    "Dynamic chain composition",
                    "Simplifies object interconnections"
                }
            };
        }

        // Iterator Pattern Demo
        public object DemonstrateIterator()
        {
            var bookCollectionDemo = _iteratorService.DemonstrateBookCollection();
            var treeIterationDemo = _iteratorService.DemonstrateTreeIteration();
            var customCollection = _iteratorService.CreateCustomBookCollection(
                new List<(string, string, string, int, decimal)>
                {
                    ("Clean Architecture", "Robert Martin", "Programming", 2017, 44.99m),
                    ("The Pragmatic Programmer", "Andy Hunt", "Programming", 1999, 39.99m),
                    ("Dune", "Frank Herbert", "Science Fiction", 1965, 16.99m)
                }
            );

            return new
            {
                Pattern = "Iterator",
                Description = "Provides way to access elements sequentially without exposing structure",
                BookCollectionDemo = bookCollectionDemo,
                TreeIterationDemo = treeIterationDemo,
                CustomCollectionDemo = customCollection,
                Example = "Book collection traversal and tree structure navigation",
                Benefits = new[]
                {
                    "Uniform traversal interface",
                    "Multiple traversal algorithms",
                    "Simplified aggregate interface"
                }
            };
        }

        // Mediator Pattern Demo
        public object DemonstrateMediator()
        {
            var chatRoomDemo = _mediatorService.DemonstrateChatRoom();
            var airTrafficDemo = _mediatorService.DemonstrateAirTrafficControl();
            var customChatDemo = _mediatorService.CreateCustomChatScenario(
                new List<string> { "Alice", "Bob", "Charlie" },
                new List<(string, string, string?)>
                {
                    ("Alice", "Hello everyone!", null),
                    ("Bob", "Hey Alice!", null),
                    ("Alice", "Can we chat privately?", "Bob"),
                    ("Charlie", "What's going on?", null)
                }
            );

            return new
            {
                Pattern = "Mediator",
                Description = "Defines how set of objects interact",
                ChatRoomDemo = chatRoomDemo,
                AirTrafficControlDemo = airTrafficDemo,
                CustomChatDemo = customChatDemo,
                Example = "Chat room communication and air traffic control",
                Benefits = new[]
                {
                    "Loose coupling between communicating objects",
                    "Centralized control logic",
                    "Reusable mediator objects"
                }
            };
        }

        // Memento Pattern Demo
        public object DemonstrateMemento()
        {
            var textEditorDemo = _mementoService.DemonstrateTextEditor();
            var gameSaveDemo = _mementoService.DemonstrateGameSaveSystem();
            var customGameDemo = _mementoService.CreateCustomGameScenario(
                "TestHero",
                new List<(string, object)>
                {
                    ("move", (10, 5)),
                    ("experience", 100),
                    ("save", "checkpoint1"),
                    ("damage", 50),
                    ("load", "checkpoint1"),
                    ("gold", 100)
                }
            );

            return new
            {
                Pattern = "Memento",
                Description = "Captures and restores object state without violating encapsulation",
                TextEditorDemo = textEditorDemo,
                GameSaveDemo = gameSaveDemo,
                CustomGameDemo = customGameDemo,
                Example = "Text editor history and game save system",
                Benefits = new[]
                {
                    "Preserves encapsulation boundaries",
                    "Simplifies originator implementation",
                    "Supports undo/redo operations"
                }
            };
        }

        // Visitor Pattern Demo
        public object DemonstrateVisitor()
        {
            var shapeVisitorDemo = _visitorService.DemonstrateShapeVisitors();
            var documentVisitorDemo = _visitorService.DemonstrateDocumentVisitor();
            var customShapeDemo = _visitorService.CreateCustomShapeScenario(
                new List<(string, Dictionary<string, object>)>
                {
                    ("circle", new Dictionary<string, object> { {"radius", 5.0}, {"x", 0.0}, {"y", 0.0}, {"color", "Red"} }),
                    ("rectangle", new Dictionary<string, object> { {"width", 10.0}, {"height", 6.0}, {"color", "Blue"} }),
                    ("triangle", new Dictionary<string, object> { {"x1", 0.0}, {"y1", 0.0}, {"x2", 4.0}, {"y2", 0.0}, {"x3", 2.0}, {"y3", 3.0}, {"color", "Green"} })
                }
            );

            return new
            {
                Pattern = "Visitor",
                Description = "Defines operations on object structure without changing classes",
                ShapeVisitorDemo = shapeVisitorDemo,
                DocumentVisitorDemo = documentVisitorDemo,
                CustomShapeDemo = customShapeDemo,
                Example = "Shape calculations and document analysis",
                Benefits = new[]
                {
                    "Easy to add new operations",
                    "Gathers related behavior in one place",
                    "Can work across class hierarchies"
                }
            };
        }

        // Complete Demo - All Patterns Together
        public object DemonstrateAllBehavioralPatterns()
        {
            return new
            {
                Title = "Behavioral Design Patterns Demonstration",
                Description = "All behavioral patterns working together in various scenarios",
                Patterns = new
                {
                    Observer = DemonstrateObserver(),
                    Strategy = DemonstrateStrategy(),
                    Command = DemonstrateCommand(),
                    State = DemonstrateState(),
                    TemplateMethod = DemonstrateTemplateMethod(),
                    ChainOfResponsibility = DemonstrateChainOfResponsibility(),
                    Iterator = DemonstrateIterator(),
                    Mediator = DemonstrateMediator(),
                    Memento = DemonstrateMemento(),
                    Visitor = DemonstrateVisitor()
                },
                Summary = new
                {
                    TotalPatterns = 10,
                    Category = "Behavioral",
                    Purpose = "Concerned with algorithms and assignment of responsibilities between objects",
                    RealWorldApplications = new[]
                    {
                        "Event handling systems (Observer)",
                        "Payment processing (Strategy)",
                        "GUI actions with undo (Command)",
                        "Workflow engines (State)",
                        "Data processing pipelines (Template Method)",
                        "Request routing (Chain of Responsibility)",
                        "Collection traversal (Iterator)",
                        "Communication systems (Mediator)",
                        "Version control (Memento)",
                        "Compiler design (Visitor)"
                    }
                }
            };
        }

        public object GetPatternSummary()
        {
            return new
            {
                Title = "Design Patterns Summary",
                Categories = new
                {
                    Creational = new
                    {
                        Count = 5,
                        Patterns = new[] { "Singleton", "Factory", "Abstract Factory", "Builder", "Prototype" },
                        Purpose = "Deal with object creation mechanisms"
                    },
                    Structural = new
                    {
                        Count = 7,
                        Patterns = new[] { "Adapter", "Bridge", "Composite", "Decorator", "Facade", "Flyweight", "Proxy" },
                        Purpose = "Deal with object composition"
                    },
                    Behavioral = new
                    {
                        Count = 10,
                        Patterns = new[] { "Observer", "Strategy", "Command", "State", "Template Method", "Chain of Responsibility", "Iterator", "Mediator", "Memento", "Visitor" },
                        Purpose = "Deal with object interaction and responsibilities"
                    }
                },
                TotalPatterns = 22,
                Benefits = new[]
                {
                    "Promotes code reuse",
                    "Makes code more maintainable",
                    "Provides common vocabulary for developers",
                    "Encapsulates best practices",
                    "Solves recurring design problems"
                }
            };
        }

        public object GetBehavioralPatternsInfo()
        {
            return new
            {
                Title = "Behavioral Design Patterns",
                Description = PatternConstants.BEHAVIORAL_DESCRIPTION,
                Patterns = PatternConstants.BEHAVIORAL_PATTERNS.Select(pattern => new
                {
                    Name = pattern,
                    Description = GetPatternDescription(pattern),
                    Endpoint = $"/api/BehavioralPatterns/{pattern.ToLower().Replace(" ", "-")}"
                }),
                UseCases = new[]
                {
                    "Event handling systems",
                    "Algorithm selection",
                    "Undo/Redo operations",
                    "Workflow management",
                    "Data processing pipelines",
                    "Request routing",
                    "Collection traversal",
                    "Communication systems",
                    "State management",
                    "AST operations"
                }
            };
        }

        private string GetPatternDescription(string pattern)
        {
            return pattern switch
            {
                "Observer" => "Defines one-to-many dependency between objects",
                "Strategy" => "Defines family of algorithms and makes them interchangeable",
                "Command" => "Encapsulates requests as objects",
                "State" => "Allows object to change behavior when internal state changes",
                "Template Method" => "Defines skeleton of algorithm, lets subclasses override specific steps",
                "Chain of Responsibility" => "Passes requests along chain of handlers",
                "Iterator" => "Provides way to access elements sequentially",
                "Mediator" => "Defines how set of objects interact",
                "Memento" => "Captures and restores object state",
                "Visitor" => "Defines operations on object structure without changing classes",
                _ => "Behavioral design pattern implementation"
            };
        }
    }
}