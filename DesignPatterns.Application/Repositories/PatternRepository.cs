using DesignPatterns.Domain.Constants;
using DesignPatterns.Domain.Data;

namespace DesignPatterns.Application.Repositories;

public interface IPatternRepository
{
    PatternInfo GetPatternInfo(string category, string name);
    PatternInfo[] GetPatternsByCategory(string category);
    string[] GetAllCategories();
    PatternInfo[] GetAllPatterns();
}

public class PatternRepository : IPatternRepository
{

    public PatternInfo GetPatternInfo(string category, string name)
    {
        return PatternData.GetPatternInfo(category, name);
    }

    public PatternInfo[] GetPatternsByCategory(string category)
    {
        return PatternData.GetPatternsByCategory(category);
    }

    public string[] GetAllCategories()
    {
        return PatternData.GetAllCategories();
    }

    public PatternInfo[] GetAllPatterns()
    {
        return PatternData.GetAllPatterns();
    }
}