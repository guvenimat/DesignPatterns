using DesignPatterns.Domain.Constants;

namespace DesignPatterns.Domain.Data;

public static class PatternData
{
    public static readonly Dictionary<string, Dictionary<string, PatternInfo>> AllPatterns = new()
    {
        {
            "creational", new Dictionary<string, PatternInfo>
            {
                { "singleton", new("Creational", "Singleton", "/api/CreationalPatterns/singleton", "Ensures a class has only one instance and provides global access to it") },
                { "factory", new("Creational", "Factory", "/api/CreationalPatterns/factory", "Creates objects without specifying the exact class of the object") },
                { "abstract factory", new("Creational", "Abstract Factory", "/api/CreationalPatterns/abstract-factory", "Provides an interface for creating families of related objects") },
                { "builder", new("Creational", "Builder", "/api/CreationalPatterns/builder", "Constructs complex objects step by step") },
                { "prototype", new("Creational", "Prototype", "/api/CreationalPatterns/prototype", "Creates objects by cloning existing instances") }
            }
        },
        {
            "structural", new Dictionary<string, PatternInfo>
            {
                { "adapter", new("Structural", "Adapter", "/api/StructuralPatterns/adapter", "Allows incompatible interfaces to work together") },
                { "bridge", new("Structural", "Bridge", "/api/StructuralPatterns/bridge", "Separates an abstraction from its implementation") },
                { "composite", new("Structural", "Composite", "/api/StructuralPatterns/composite", "Composes objects into tree structures to represent part-whole hierarchies") },
                { "decorator", new("Structural", "Decorator", "/api/StructuralPatterns/decorator", "Adds behavior to objects dynamically without altering their structure") },
                { "facade", new("Structural", "Facade", "/api/StructuralPatterns/facade", "Provides a simplified interface to a complex subsystem") },
                { "flyweight", new("Structural", "Flyweight", "/api/StructuralPatterns/flyweight", "Minimizes memory usage by sharing efficiently among similar objects") },
                { "proxy", new("Structural", "Proxy", "/api/StructuralPatterns/proxy", "Provides a placeholder or surrogate to control access to another object") }
            }
        },
        {
            "behavioral", new Dictionary<string, PatternInfo>
            {
                { "observer", new("Behavioral", "Observer", "/api/BehavioralPatterns/observer", "Defines a one-to-many dependency between objects") },
                { "strategy", new("Behavioral", "Strategy", "/api/BehavioralPatterns/strategy", "Defines a family of algorithms and makes them interchangeable") },
                { "command", new("Behavioral", "Command", "/api/BehavioralPatterns/command", "Encapsulates a request as an object") },
                { "state", new("Behavioral", "State", "/api/BehavioralPatterns/state", "Allows an object to alter its behavior when its internal state changes") },
                { "template method", new("Behavioral", "Template Method", "/api/BehavioralPatterns/template-method", "Defines the skeleton of an algorithm in a base class") },
                { "chain of responsibility", new("Behavioral", "Chain of Responsibility", "/api/BehavioralPatterns/chain-of-responsibility", "Passes requests along a chain of handlers") },
                { "iterator", new("Behavioral", "Iterator", "/api/BehavioralPatterns/iterator", "Provides a way to access elements of an aggregate object sequentially") },
                { "mediator", new("Behavioral", "Mediator", "/api/BehavioralPatterns/mediator", "Defines how a set of objects interact with each other") },
                { "memento", new("Behavioral", "Memento", "/api/BehavioralPatterns/memento", "Captures and restores an object's internal state") },
                { "visitor", new("Behavioral", "Visitor", "/api/BehavioralPatterns/visitor", "Defines operations to be performed on elements of an object structure") }
            }
        }
    };

    public static PatternInfo GetPatternInfo(string category, string name)
    {
        var normalizedCategory = category.ToLower();
        var normalizedName = name.ToLower();

        if (!AllPatterns.ContainsKey(normalizedCategory))
            throw new ArgumentException($"Unknown pattern category: {category}");

        if (!AllPatterns[normalizedCategory].ContainsKey(normalizedName))
            throw new ArgumentException($"Unknown pattern '{name}' in category '{category}'");

        return AllPatterns[normalizedCategory][normalizedName];
    }

    public static PatternInfo[] GetPatternsByCategory(string category)
    {
        var normalizedCategory = category.ToLower();

        if (!AllPatterns.ContainsKey(normalizedCategory))
            throw new ArgumentException($"Unknown pattern category: {category}");

        return AllPatterns[normalizedCategory].Values.ToArray();
    }

    public static string[] GetAllCategories()
    {
        return AllPatterns.Keys.Select(k => char.ToUpper(k[0]) + k[1..]).ToArray();
    }

    public static PatternInfo[] GetAllPatterns()
    {
        return AllPatterns.Values
            .SelectMany(categoryPatterns => categoryPatterns.Values)
            .ToArray();
    }
}