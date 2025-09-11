using DesignPatterns.Client.Models;
using System.Text.Json;

namespace DesignPatterns.Client.Services
{
    public interface IDesignPatternsApiService
    {
        Task<ApiWelcomeResponse?> GetWelcomeAsync();
        Task<PatternExecutionResult> ExecutePatternAsync(string category, string pattern);
        Task<PatternExecutionResult> ExecuteAllPatternsAsync(string category);
        Task<object?> GetPatternInfoAsync(string category);
        Task<PatternExecutionResult> GetRandomPatternAsync();
    }

    public class DesignPatternsApiService : IDesignPatternsApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<DesignPatternsApiService> _logger;
        private readonly JsonSerializerOptions _jsonOptions;

        public DesignPatternsApiService(HttpClient httpClient, ILogger<DesignPatternsApiService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
        }

        public async Task<ApiWelcomeResponse?> GetWelcomeAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/DesignPatterns");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<ApiWelcomeResponse>(content, _jsonOptions);
                }
                _logger.LogWarning("Failed to get welcome info. Status: {StatusCode}", response.StatusCode);
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting welcome info");
                return null;
            }
        }

        public async Task<PatternExecutionResult> ExecutePatternAsync(string category, string pattern)
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            try
            {
                var endpoint = $"api/{category}Patterns/{pattern}";
                var response = await _httpClient.GetAsync(endpoint);
                stopwatch.Stop();

                var content = await response.Content.ReadAsStringAsync();
                
                if (response.IsSuccessStatusCode)
                {
                    return new PatternExecutionResult
                    {
                        Pattern = $"{category} - {pattern}",
                        Description = $"Executed {pattern} pattern from {category} category",
                        Result = JsonSerializer.Deserialize<object>(content, _jsonOptions),
                        Success = true,
                        ExecutedAt = DateTime.Now,
                        ExecutionTime = stopwatch.Elapsed,
                        FormattedResult = FormatJson(content)
                    };
                }
                else
                {
                    return new PatternExecutionResult
                    {
                        Pattern = $"{category} - {pattern}",
                        Description = $"Failed to execute {pattern} pattern",
                        Success = false,
                        Error = $"HTTP {response.StatusCode}: {content}",
                        ExecutedAt = DateTime.Now,
                        ExecutionTime = stopwatch.Elapsed
                    };
                }
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.LogError(ex, "Error executing pattern {Pattern} from {Category}", pattern, category);
                return new PatternExecutionResult
                {
                    Pattern = $"{category} - {pattern}",
                    Description = $"Error executing {pattern} pattern",
                    Success = false,
                    Error = ex.Message,
                    ExecutedAt = DateTime.Now,
                    ExecutionTime = stopwatch.Elapsed
                };
            }
        }

        public async Task<PatternExecutionResult> ExecuteAllPatternsAsync(string category)
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            try
            {
                var endpoint = $"api/{category}Patterns/all";
                var response = await _httpClient.GetAsync(endpoint);
                stopwatch.Stop();

                var content = await response.Content.ReadAsStringAsync();
                
                if (response.IsSuccessStatusCode)
                {
                    return new PatternExecutionResult
                    {
                        Pattern = $"All {category} Patterns",
                        Description = $"Executed all patterns from {category} category",
                        Result = JsonSerializer.Deserialize<object>(content, _jsonOptions),
                        Success = true,
                        ExecutedAt = DateTime.Now,
                        ExecutionTime = stopwatch.Elapsed,
                        FormattedResult = FormatJson(content)
                    };
                }
                else
                {
                    return new PatternExecutionResult
                    {
                        Pattern = $"All {category} Patterns",
                        Description = $"Failed to execute all {category} patterns",
                        Success = false,
                        Error = $"HTTP {response.StatusCode}: {content}",
                        ExecutedAt = DateTime.Now,
                        ExecutionTime = stopwatch.Elapsed
                    };
                }
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.LogError(ex, "Error executing all patterns from {Category}", category);
                return new PatternExecutionResult
                {
                    Pattern = $"All {category} Patterns",
                    Description = $"Error executing all {category} patterns",
                    Success = false,
                    Error = ex.Message,
                    ExecutedAt = DateTime.Now,
                    ExecutionTime = stopwatch.Elapsed
                };
            }
        }

        public async Task<object?> GetPatternInfoAsync(string category)
        {
            try
            {
                var endpoint = $"api/{category}Patterns/info";
                var response = await _httpClient.GetAsync(endpoint);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<object>(content, _jsonOptions);
                }
                _logger.LogWarning("Failed to get pattern info for {Category}. Status: {StatusCode}", category, response.StatusCode);
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting pattern info for {Category}", category);
                return null;
            }
        }

        public async Task<PatternExecutionResult> GetRandomPatternAsync()
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            try
            {
                var response = await _httpClient.GetAsync("api/DesignPatterns/random-pattern");
                stopwatch.Stop();

                var content = await response.Content.ReadAsStringAsync();
                
                if (response.IsSuccessStatusCode)
                {
                    return new PatternExecutionResult
                    {
                        Pattern = "Random Pattern Recommendation",
                        Description = "Retrieved a random pattern recommendation",
                        Result = JsonSerializer.Deserialize<object>(content, _jsonOptions),
                        Success = true,
                        ExecutedAt = DateTime.Now,
                        ExecutionTime = stopwatch.Elapsed,
                        FormattedResult = FormatJson(content)
                    };
                }
                else
                {
                    return new PatternExecutionResult
                    {
                        Pattern = "Random Pattern Recommendation",
                        Description = "Failed to get random pattern",
                        Success = false,
                        Error = $"HTTP {response.StatusCode}: {content}",
                        ExecutedAt = DateTime.Now,
                        ExecutionTime = stopwatch.Elapsed
                    };
                }
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.LogError(ex, "Error getting random pattern");
                return new PatternExecutionResult
                {
                    Pattern = "Random Pattern Recommendation",
                    Description = "Error getting random pattern",
                    Success = false,
                    Error = ex.Message,
                    ExecutedAt = DateTime.Now,
                    ExecutionTime = stopwatch.Elapsed
                };
            }
        }

        private string FormatJson(string json)
        {
            try
            {
                var jsonDoc = JsonDocument.Parse(json);
                return JsonSerializer.Serialize(jsonDoc, new JsonSerializerOptions { WriteIndented = true });
            }
            catch
            {
                return json;
            }
        }
    }
}