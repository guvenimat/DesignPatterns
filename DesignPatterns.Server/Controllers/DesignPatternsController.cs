using Microsoft.AspNetCore.Mvc;
using DesignPatterns.Application.Services;

namespace DesignPatterns.Server.Controllers
{
    /// <summary>
    /// Main controller providing overview and navigation for the Design Patterns API
    /// </summary>
    [Route("api/[controller]")]
    public class DesignPatternsController : BaseController
    {
        private readonly IPatternInformationService _patternInformationService;

        public DesignPatternsController(IPatternInformationService patternInformationService)
        {
            _patternInformationService = patternInformationService;
        }
        /// <summary>
        /// Welcome endpoint providing overview of the Design Patterns API
        /// </summary>
        [HttpGet]
        public IActionResult GetWelcome()
        {
            var welcome = _patternInformationService.GetWelcomeInformation();
            return Success(welcome);
        }

        /// <summary>
        /// Get all design patterns organized by category
        /// </summary>
        [HttpGet("all-patterns")]
        public IActionResult GetAllPatterns()
        {
            var patterns = _patternInformationService.GetAllPatternsInformation();
            return Success(patterns);
        }

        /// <summary>
        /// Health check endpoint to verify API is running
        /// </summary>
        [HttpGet("health")]
        public IActionResult HealthCheck()
        {
            var health = _patternInformationService.GetHealthCheckInformation();
            return Success(health);
        }

        /// <summary>
        /// Get random pattern recommendation
        /// </summary>
        [HttpGet("random-pattern")]
        public IActionResult GetRandomPattern()
        {
            var recommendation = _patternInformationService.GetRandomPatternRecommendation();
            return Success(recommendation);
        }

        /// <summary>
        /// Get learning path recommendations
        /// </summary>
        [HttpGet("learning-path")]
        public IActionResult GetLearningPath()
        {
            var learningPath = _patternInformationService.GetLearningPath();
            return Success(learningPath);
        }
    }
}