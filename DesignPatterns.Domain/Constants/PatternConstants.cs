namespace DesignPatterns.Domain.Constants;

/// <summary>
/// Constants for design patterns application
/// </summary>
public static class PatternConstants
{
    // API Information
    public const string API_TITLE = "Design Patterns Learning API";
    public const string API_DESCRIPTION = "A comprehensive .NET API demonstrating all major design patterns";
    public const string API_VERSION = "1.0.0";
    public const string API_AUTHOR = "Clean Architecture Demo";

    // Pattern Categories
    public const string CREATIONAL_DESCRIPTION = "Deal with object creation mechanisms";
    public const string STRUCTURAL_DESCRIPTION = "Deal with object composition and relationships";
    public const string BEHAVIORAL_DESCRIPTION = "Deal with object interaction and responsibilities";

    // Endpoints
    public const string CREATIONAL_ENDPOINT = "/api/CreationalPatterns";
    public const string STRUCTURAL_ENDPOINT = "/api/StructuralPatterns";
    public const string BEHAVIORAL_ENDPOINT = "/api/BehavioralPatterns";

    // Pattern Lists
    public static readonly string[] CREATIONAL_PATTERNS = 
    {
        "Singleton", "Factory", "Abstract Factory", "Builder", "Prototype"
    };

    public static readonly string[] STRUCTURAL_PATTERNS = 
    {
        "Adapter", "Bridge", "Composite", "Decorator", "Facade", "Flyweight", "Proxy"
    };

    public static readonly string[] BEHAVIORAL_PATTERNS = 
    {
        "Observer", "Strategy", "Command", "State", "Template Method", 
        "Chain of Responsibility", "Iterator", "Mediator", "Memento", "Visitor"
    };

    public const int TOTAL_PATTERNS = 22;

    // API Features
    public static readonly string[] API_FEATURES = 
    {
        "All 22 GoF Design Patterns implemented",
        "Clean Architecture with separation of concerns",
        "Comprehensive examples with real-world scenarios",
        "Debug-friendly code with detailed explanations",
        "RESTful API endpoints for each pattern",
        "Custom scenario creation capabilities",
        "No CQRS or MediatR dependencies as requested"
    };

    // Learning Benefits
    public static readonly string[] LEARNING_BENEFITS = 
    {
        "Learn a new design pattern",
        "Expand your architectural knowledge",
        "See practical implementation examples",
        "Understand real-world use cases"
    };

    // Study Tips
    public static readonly string[] STUDY_TIPS = 
    {
        "Start with simple patterns and build complexity gradually",
        "Always understand the problem before learning the solution",
        "Practice implementing patterns in your own projects",
        "Focus on when to use each pattern, not just how",
        "Remember: patterns are tools, not rules - use them wisely"
    };

    // Sample Patterns for Random Recommendation
    public static readonly PatternInfo[] SAMPLE_PATTERNS = 
    {
        new("Creational", "Singleton", "/api/CreationalPatterns/singleton", "Perfect for managing global state like configuration"),
        new("Creational", "Factory", "/api/CreationalPatterns/factory", "Great for creating objects without knowing exact types"),
        new("Structural", "Adapter", "/api/StructuralPatterns/adapter", "Essential for integrating with legacy systems"),
        new("Structural", "Decorator", "/api/StructuralPatterns/decorator", "Perfect for adding features without inheritance"),
        new("Behavioral", "Observer", "/api/BehavioralPatterns/observer", "Ideal for event-driven architectures"),
        new("Behavioral", "Strategy", "/api/BehavioralPatterns/strategy", "Great for runtime algorithm selection"),
        new("Behavioral", "Command", "/api/BehavioralPatterns/command", "Perfect for implementing undo/redo functionality")
    };

    // Learning Path Constants
    public static readonly LearningLevel BEGINNER_PATTERNS = new("Beginner", new[]
    {
        new LearningPattern(1, "Singleton", "Simple concept, widely used", "/api/CreationalPatterns/singleton"),
        new LearningPattern(2, "Factory", "Fundamental creation pattern", "/api/CreationalPatterns/factory"),
        new LearningPattern(3, "Observer", "Important for event handling", "/api/BehavioralPatterns/observer"),
        new LearningPattern(4, "Strategy", "Easy to understand and very useful", "/api/BehavioralPatterns/strategy"),
        new LearningPattern(5, "Decorator", "Great alternative to inheritance", "/api/StructuralPatterns/decorator")
    });

    public static readonly LearningLevel INTERMEDIATE_PATTERNS = new("Intermediate", new[]
    {
        new LearningPattern(6, "Builder", "Complex object construction", "/api/CreationalPatterns/builder"),
        new LearningPattern(7, "Adapter", "Common in integration scenarios", "/api/StructuralPatterns/adapter"),
        new LearningPattern(8, "Command", "Powerful for undo/redo and queuing", "/api/BehavioralPatterns/command"),
        new LearningPattern(9, "State", "Important for state machines", "/api/BehavioralPatterns/state"),
        new LearningPattern(10, "Template Method", "Framework design pattern", "/api/BehavioralPatterns/template-method")
    });

    public static readonly LearningLevel ADVANCED_PATTERNS = new("Advanced", new[]
    {
        new LearningPattern(11, "Abstract Factory", "Complex creation scenarios", "/api/CreationalPatterns/abstract-factory"),
        new LearningPattern(12, "Composite", "Tree structures and hierarchies", "/api/StructuralPatterns/composite"),
        new LearningPattern(13, "Chain of Responsibility", "Request processing chains", "/api/BehavioralPatterns/chain-of-responsibility"),
        new LearningPattern(14, "Visitor", "Operations on object structures", "/api/BehavioralPatterns/visitor"),
        new LearningPattern(15, "Mediator", "Complex object interactions", "/api/BehavioralPatterns/mediator")
    });

    public static readonly LearningLevel EXPERT_PATTERNS = new("Expert", new[]
    {
        new LearningPattern(16, "Prototype", "Cloning and performance optimization", "/api/CreationalPatterns/prototype"),
        new LearningPattern(17, "Flyweight", "Memory optimization techniques", "/api/StructuralPatterns/flyweight"),
        new LearningPattern(18, "Proxy", "Access control and lazy loading", "/api/StructuralPatterns/proxy"),
        new LearningPattern(19, "Iterator", "Custom collection traversal", "/api/BehavioralPatterns/iterator"),
        new LearningPattern(20, "Memento", "State preservation and restoration", "/api/BehavioralPatterns/memento")
    });

    // Response Messages
    public const string SUCCESS_MESSAGE = "Operation completed successfully";
    public const string VALIDATION_ERROR_MESSAGE = "Validation failed";
    public const string NOT_FOUND_MESSAGE = "Resource not found";
    public const string FORBIDDEN_MESSAGE = "Access forbidden";

    // HTTP Status Messages
    public const string HEALTHY_STATUS = "Healthy";
    public const string AVAILABLE_STATUS = "Available";
}

/// <summary>
/// Represents pattern information for recommendations
/// </summary>
/// <param name="Category">Pattern category</param>
/// <param name="Name">Pattern name</param>
/// <param name="Endpoint">API endpoint</param>
/// <param name="Description">Pattern description</param>
public record PatternInfo(string Category, string Name, string Endpoint, string Description);

/// <summary>
/// Represents a learning pattern with order and reasoning
/// </summary>
/// <param name="Order">Learning order</param>
/// <param name="Pattern">Pattern name</param>
/// <param name="Reason">Why to learn this pattern</param>
/// <param name="Endpoint">API endpoint</param>
public record LearningPattern(int Order, string Pattern, string Reason, string Endpoint);

/// <summary>
/// Represents a learning level with patterns
/// </summary>
/// <param name="Level">Learning level name</param>
/// <param name="Patterns">Patterns for this level</param>
public record LearningLevel(string Level, LearningPattern[] Patterns);