using DesignPatterns.Domain.Constants;
using DesignPatterns.Application.Repositories;

namespace DesignPatterns.Application.Services;

public interface IPatternInformationService
{
    object GetWelcomeInformation();
    object GetAllPatternsInformation();
    object GetHealthCheckInformation();
    object GetRandomPatternRecommendation();
    object GetLearningPath();
    object GetPatternsByCategory(string category);
}

public class PatternInformationService : IPatternInformationService
{
    private readonly IPatternRepository _patternRepository;

    public PatternInformationService(IPatternRepository patternRepository)
    {
        _patternRepository = patternRepository;
    }
    public object GetWelcomeInformation()
    {
        return new
        {
            Title = PatternConstants.API_TITLE,
            Description = PatternConstants.API_DESCRIPTION,
            Version = PatternConstants.API_VERSION,
            Author = PatternConstants.API_AUTHOR,
            Categories = new
            {
                Creational = new
                {
                    Description = PatternConstants.CREATIONAL_DESCRIPTION,
                    Endpoint = PatternConstants.CREATIONAL_ENDPOINT,
                    Patterns = PatternConstants.CREATIONAL_PATTERNS,
                    Count = PatternConstants.CREATIONAL_PATTERNS.Length
                },
                Structural = new
                {
                    Description = PatternConstants.STRUCTURAL_DESCRIPTION,
                    Endpoint = PatternConstants.STRUCTURAL_ENDPOINT,
                    Patterns = PatternConstants.STRUCTURAL_PATTERNS,
                    Count = PatternConstants.STRUCTURAL_PATTERNS.Length
                },
                Behavioral = new
                {
                    Description = PatternConstants.BEHAVIORAL_DESCRIPTION,
                    Endpoint = PatternConstants.BEHAVIORAL_ENDPOINT,
                    Patterns = PatternConstants.BEHAVIORAL_PATTERNS,
                    Count = PatternConstants.BEHAVIORAL_PATTERNS.Length
                }
            },
            TotalPatterns = PatternConstants.TOTAL_PATTERNS,
            QuickStart = GetQuickStartInformation(),
            Features = PatternConstants.API_FEATURES
        };
    }

    public object GetAllPatternsInformation()
    {
        return new
        {
            Title = "Complete Design Patterns Reference",
            CreationalPatterns = GetCreationalPatternsInfo(),
            StructuralPatterns = GetStructuralPatternsInfo(),
            BehavioralPatterns = GetBehavioralPatternsInfo()
        };
    }

    public object GetHealthCheckInformation()
    {
        return new
        {
            Status = "Healthy",
            Timestamp = DateTime.UtcNow,
            Version = PatternConstants.API_VERSION,
            Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production",
            Uptime = TimeSpan.FromMilliseconds(Environment.TickCount64),
            Services = new
            {
                CreationalPatterns = "Available",
                StructuralPatterns = "Available",
                BehavioralPatterns = "Available"
            },
            Memory = GetMemoryInformation()
        };
    }

    public object GetRandomPatternRecommendation()
    {
        var patterns = PatternConstants.SAMPLE_PATTERNS;
        var random = new Random();
        var selectedPattern = patterns[random.Next(patterns.Length)];

        return new
        {
            Title = "Random Pattern Recommendation",
            Pattern = selectedPattern,
            WhyUseIt = PatternConstants.LEARNING_BENEFITS,
            NextSteps = GetNextSteps(selectedPattern.Endpoint)
        };
    }

    public object GetLearningPath()
    {
        return new
        {
            Title = "Design Patterns Learning Path",
            Description = "Recommended order for learning design patterns",
            Beginner = PatternConstants.BEGINNER_PATTERNS,
            Intermediate = PatternConstants.INTERMEDIATE_PATTERNS,
            Advanced = PatternConstants.ADVANCED_PATTERNS,
            Expert = PatternConstants.EXPERT_PATTERNS,
            StudyTips = PatternConstants.STUDY_TIPS
        };
    }

    public object GetPatternsByCategory(string category)
    {
        return category.ToLower() switch
        {
            "creational" => GetCreationalPatternsInfo(),
            "structural" => GetStructuralPatternsInfo(),
            "behavioral" => GetBehavioralPatternsInfo(),
            _ => throw new ArgumentException($"Unknown pattern category: {category}")
        };
    }

    private object GetQuickStartInformation()
    {
        return new
        {
            GetAllPatterns = "/api/DesignPatterns/all-patterns",
            CreationalDemo = "/api/CreationalPatterns/all",
            StructuralDemo = "/api/StructuralPatterns/all",
            BehavioralDemo = "/api/BehavioralPatterns/all",
            PatternSummary = "/api/BehavioralPatterns/summary",
            HealthCheck = "/api/DesignPatterns/health"
        };
    }

    private object GetCreationalPatternsInfo()
    {
        return PatternConstants.CREATIONAL_PATTERNS.Select(pattern => new
        {
            Name = pattern,
            Purpose = GetPatternPurpose("creational", pattern),
            Example = GetPatternExample("creational", pattern),
            Endpoint = $"/api/CreationalPatterns/{pattern.ToLower().Replace(" ", "-")}"
        });
    }

    private object GetStructuralPatternsInfo()
    {
        return PatternConstants.STRUCTURAL_PATTERNS.Select(pattern => new
        {
            Name = pattern,
            Purpose = GetPatternPurpose("structural", pattern),
            Example = GetPatternExample("structural", pattern),
            Endpoint = $"/api/StructuralPatterns/{pattern.ToLower().Replace(" ", "-")}"
        });
    }

    private object GetBehavioralPatternsInfo()
    {
        return PatternConstants.BEHAVIORAL_PATTERNS.Select(pattern => new
        {
            Name = pattern,
            Purpose = GetPatternPurpose("behavioral", pattern),
            Example = GetPatternExample("behavioral", pattern),
            Endpoint = $"/api/BehavioralPatterns/{pattern.ToLower().Replace(" ", "-")}"
        });
    }

    private object GetMemoryInformation()
    {
        return new
        {
            WorkingSet = GC.GetTotalMemory(false),
            Gen0Collections = GC.CollectionCount(0),
            Gen1Collections = GC.CollectionCount(1),
            Gen2Collections = GC.CollectionCount(2)
        };
    }

    private string[] GetNextSteps(string endpoint)
    {
        return new[]
        {
            $"Visit {endpoint} to see it in action",
            "Try the debug mode to step through the code",
            "Read the pattern description and benefits",
            "Think about where you could apply it in your projects"
        };
    }

    private string GetPatternPurpose(string category, string pattern)
    {
        // This could be moved to a dictionary or configuration
        return $"{pattern} purpose for {category} patterns";
    }

    private string GetPatternExample(string category, string pattern)
    {
        // This could be moved to a dictionary or configuration
        return $"{pattern} example for {category} patterns";
    }
}