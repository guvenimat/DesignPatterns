namespace DesignPatterns.Domain.Exceptions;

/// <summary>
/// Base exception for all design pattern related exceptions
/// </summary>
public abstract class DesignPatternException : Exception
{
    protected DesignPatternException(string message) : base(message) { }
    protected DesignPatternException(string message, Exception innerException) : base(message, innerException) { }
}

/// <summary>
/// Exception thrown when a pattern demonstration fails
/// </summary>
public class PatternDemonstrationException : DesignPatternException
{
    public string PatternName { get; }
    
    public PatternDemonstrationException(string patternName, string message) 
        : base($"Pattern demonstration failed for {patternName}: {message}")
    {
        PatternName = patternName;
    }
    
    public PatternDemonstrationException(string patternName, string message, Exception innerException) 
        : base($"Pattern demonstration failed for {patternName}: {message}", innerException)
    {
        PatternName = patternName;
    }
}

/// <summary>
/// Exception thrown when invalid pattern type is requested
/// </summary>
public class InvalidPatternTypeException : DesignPatternException
{
    public string RequestedType { get; }
    public string[] AvailableTypes { get; }
    
    public InvalidPatternTypeException(string requestedType, string[] availableTypes) 
        : base($"Invalid pattern type '{requestedType}'. Available types: {string.Join(", ", availableTypes)}")
    {
        RequestedType = requestedType;
        AvailableTypes = availableTypes;
    }
}

/// <summary>
/// Exception thrown when pattern configuration is invalid
/// </summary>
public class PatternConfigurationException : DesignPatternException
{
    public string ConfigurationKey { get; }
    
    public PatternConfigurationException(string configurationKey, string message) 
        : base($"Configuration error for {configurationKey}: {message}")
    {
        ConfigurationKey = configurationKey;
    }
}