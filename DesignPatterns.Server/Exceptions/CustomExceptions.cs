namespace DesignPatterns.Server.Exceptions;

/// <summary>
/// Base exception for all design patterns application exceptions
/// </summary>
public abstract class DesignPatternsException : Exception
{
    protected DesignPatternsException(string message) : base(message) { }
    protected DesignPatternsException(string message, Exception innerException) : base(message, innerException) { }
}

/// <summary>
/// Exception thrown when a design pattern demonstration fails
/// </summary>
public class PatternDemonstrationException : DesignPatternsException
{
    public string PatternName { get; }
    
    public PatternDemonstrationException(string patternName, string message) 
        : base($"Failed to demonstrate {patternName} pattern: {message}")
    {
        PatternName = patternName;
    }
    
    public PatternDemonstrationException(string patternName, string message, Exception innerException) 
        : base($"Failed to demonstrate {patternName} pattern: {message}", innerException)
    {
        PatternName = patternName;
    }
}

/// <summary>
/// Exception thrown when a business rule validation fails
/// </summary>
public class BusinessRuleException : DesignPatternsException
{
    public string RuleName { get; }
    
    public BusinessRuleException(string ruleName, string message) 
        : base($"Business rule '{ruleName}' violated: {message}")
    {
        RuleName = ruleName;
    }
}

/// <summary>
/// Exception thrown when a configuration error occurs
/// </summary>
public class ConfigurationException : DesignPatternsException
{
    public string ConfigurationKey { get; }
    
    public ConfigurationException(string configurationKey, string message) 
        : base($"Configuration error for '{configurationKey}': {message}")
    {
        ConfigurationKey = configurationKey;
    }
}

/// <summary>
/// Exception thrown when a service is unavailable
/// </summary>
public class ServiceUnavailableException : DesignPatternsException
{
    public string ServiceName { get; }
    
    public ServiceUnavailableException(string serviceName, string message) 
        : base($"Service '{serviceName}' is unavailable: {message}")
    {
        ServiceName = serviceName;
    }
    
    public ServiceUnavailableException(string serviceName, string message, Exception innerException) 
        : base($"Service '{serviceName}' is unavailable: {message}", innerException)
    {
        ServiceName = serviceName;
    }
}