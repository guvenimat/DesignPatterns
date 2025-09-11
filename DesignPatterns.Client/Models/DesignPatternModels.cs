namespace DesignPatterns.Client.Models
{
    public class DesignPatternCategory
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Endpoint { get; set; } = string.Empty;
        public List<DesignPattern> Patterns { get; set; } = new();
        public int Count { get; set; }
        public string Icon { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
    }

    public class DesignPattern
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Purpose { get; set; } = string.Empty;
        public string Example { get; set; } = string.Empty;
        public string Endpoint { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public List<string> Benefits { get; set; } = new();
        public List<string> UseCases { get; set; } = new();
        public string Icon { get; set; } = string.Empty;
        public string Difficulty { get; set; } = "Beginner";
    }

    public class PatternExecutionResult
    {
        public string Pattern { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public object? Result { get; set; }
        public bool Success { get; set; }
        public string? Error { get; set; }
        public DateTime ExecutedAt { get; set; } = DateTime.Now;
        public TimeSpan ExecutionTime { get; set; }
        public string FormattedResult { get; set; } = string.Empty;
    }

    public class ApiWelcomeResponse
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public WelcomeCategories Categories { get; set; } = new();
        public int TotalPatterns { get; set; }
        public QuickStartLinks QuickStart { get; set; } = new();
        public List<string> Features { get; set; } = new();
    }

    public class WelcomeCategories
    {
        public CategoryInfo Creational { get; set; } = new();
        public CategoryInfo Structural { get; set; } = new();
        public CategoryInfo Behavioral { get; set; } = new();
    }

    public class CategoryInfo
    {
        public string Description { get; set; } = string.Empty;
        public string Endpoint { get; set; } = string.Empty;
        public List<string> Patterns { get; set; } = new();
        public int Count { get; set; }
    }

    public class QuickStartLinks
    {
        public string GetAllPatterns { get; set; } = string.Empty;
        public string CreationalDemo { get; set; } = string.Empty;
        public string StructuralDemo { get; set; } = string.Empty;
        public string BehavioralDemo { get; set; } = string.Empty;
        public string PatternSummary { get; set; } = string.Empty;
    }
}