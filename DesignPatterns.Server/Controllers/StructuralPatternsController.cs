﻿﻿using Microsoft.AspNetCore.Mvc;
using DesignPatterns.Application.Interfaces;

namespace DesignPatterns.Server.Controllers
{
    /// <summary>
    /// Controller for demonstrating structural design patterns
    /// </summary>
    [Route("api/[controller]")]
    public class StructuralPatternsController : BaseController
    {
        private readonly IStructuralPatternsService _service;

        public StructuralPatternsController(IStructuralPatternsService service)
        {
            _service = service;
        }

        /// <summary>
        /// Demonstrates the Adapter pattern with payment system integration
        /// </summary>
        [HttpGet("adapter")]
        public IActionResult DemonstrateAdapter()
        {
            var result = _service.DemonstrateAdapter();
            return Success(result);
        }

        /// <summary>
        /// Demonstrates the Bridge pattern with notification systems
        /// </summary>
        [HttpGet("bridge")]
        public IActionResult DemonstrateBridge()
        {
            var result = _service.DemonstrateBridge();
            return Success(result);
        }

        /// <summary>
        /// Demonstrates the Composite pattern with file system hierarchy
        /// </summary>
        [HttpGet("composite")]
        public IActionResult DemonstrateComposite()
        {
            var result = _service.DemonstrateComposite();
            return Success(result);
        }

        /// <summary>
        /// Demonstrates the Decorator pattern with coffee shop customization
        /// </summary>
        [HttpGet("decorator")]
        public IActionResult DemonstrateDecorator()
        {
            var result = _service.DemonstrateDecorator();
            return Success(result);
        }

        /// <summary>
        /// Demonstrates the Facade pattern with order processing
        /// </summary>
        [HttpGet("facade")]
        public IActionResult DemonstrateFacade()
        {
            var result = _service.DemonstrateFacade();
            return Success(result);
        }

        /// <summary>
        /// Demonstrates the Flyweight pattern with text editor optimization
        /// </summary>
        [HttpGet("flyweight")]
        public IActionResult DemonstrateFlyweight()
        {
            var result = _service.DemonstrateFlyweight();
            return Success(result);
        }

        /// <summary>
        /// Demonstrates the Proxy pattern with image loading scenarios
        /// </summary>
        [HttpGet("proxy")]
        public IActionResult DemonstrateProxy()
        {
            var result = _service.DemonstrateProxy();
            return Success(result);
        }

        /// <summary>
        /// Demonstrates all structural patterns together
        /// </summary>
        [HttpGet("all")]
        public IActionResult DemonstrateAllStructuralPatterns()
        {
            var result = _service.DemonstrateAllStructuralPatterns();
            return Success(result);
        }

        /// <summary>
        /// Creates a custom scenario for a specific structural pattern
        /// </summary>
        [HttpPost("custom-scenario")]
        public IActionResult CreateCustomScenario([FromBody] CustomScenarioRequest request)
        {
            if (string.IsNullOrEmpty(request.PatternType))
                return ValidationError("Pattern type is required");

            var result = _service.CreateCustomScenario(
                request.PatternType,
                request.Parameters ?? new Dictionary<string, object>());
            
            return Success(result);
        }

        /// <summary>
        /// Get information about all structural patterns
        /// </summary>
        [HttpGet("info")]
        public IActionResult GetStructuralPatternsInfo()
        {
            var info = new
            {
                Title = "Structural Design Patterns",
                Description = "Structural design patterns are concerned with how classes and objects are composed to form larger structures. They simplify the design by identifying simple ways to realize relationships between entities.",
                Patterns = new[]
                {
                    new { Name = "Adapter", Description = "Allows incompatible interfaces to work together", Endpoint = "/api/StructuralPatterns/adapter" },
                    new { Name = "Bridge", Description = "Separates abstraction from implementation", Endpoint = "/api/StructuralPatterns/bridge" },
                    new { Name = "Composite", Description = "Composes objects into tree structures", Endpoint = "/api/StructuralPatterns/composite" },
                    new { Name = "Decorator", Description = "Adds behavior to objects dynamically", Endpoint = "/api/StructuralPatterns/decorator" },
                    new { Name = "Facade", Description = "Provides simplified interface to complex subsystem", Endpoint = "/api/StructuralPatterns/facade" },
                    new { Name = "Flyweight", Description = "Minimizes memory usage by sharing efficiently", Endpoint = "/api/StructuralPatterns/flyweight" },
                    new { Name = "Proxy", Description = "Provides placeholder to control access to another object", Endpoint = "/api/StructuralPatterns/proxy" }
                },
                UseCases = new[]
                {
                    "Legacy system integration",
                    "Cross-platform development",
                    "UI component hierarchies",
                    "Feature enhancement",
                    "System API simplification",
                    "Memory optimization",
                    "Remote service access"
                }
            };
            return Ok(info);
        }
    }

    public class CustomScenarioRequest
    {
        public string PatternType { get; set; } = string.Empty;
        public Dictionary<string, object>? Parameters { get; set; }
    }
}
