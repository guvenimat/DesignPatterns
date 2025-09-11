﻿﻿using Microsoft.AspNetCore.Mvc;
using DesignPatterns.Application.Interfaces;

namespace DesignPatterns.Server.Controllers
{
    /// <summary>
    /// Controller for demonstrating creational design patterns
    /// </summary>
    [Route("api/[controller]")]
    public class CreationalPatternsController : BaseController
    {
        private readonly ICreationalPatternsService _service;

        public CreationalPatternsController(ICreationalPatternsService service)
        {
            _service = service;
        }

        /// <summary>
        /// Demonstrates the Singleton pattern with configuration management
        /// </summary>
        [HttpGet("singleton")]
        public IActionResult DemonstrateSingleton()
        {
            var result = _service.DemonstrateSingleton();
            return Success(result);
        }

        /// <summary>
        /// Demonstrates Factory and Factory Method patterns
        /// </summary>
        [HttpGet("factory")]
        public IActionResult DemonstrateFactory()
        {
            var result = _service.DemonstrateFactory();
            return Success(result);
        }

        /// <summary>
        /// Demonstrates Abstract Factory pattern with different store types
        /// </summary>
        [HttpGet("abstract-factory")]
        public IActionResult DemonstrateAbstractFactory()
        {
            var result = _service.DemonstrateAbstractFactory();
            return Success(result);
        }

        /// <summary>
        /// Demonstrates Builder pattern for complex object construction
        /// </summary>
        [HttpGet("builder")]
        public IActionResult DemonstrateBuilder()
        {
            var result = _service.DemonstrateBuilder();
            return Success(result);
        }

        /// <summary>
        /// Demonstrates Prototype pattern with object cloning
        /// </summary>
        [HttpGet("prototype")]
        public IActionResult DemonstratePrototype()
        {
            var result = _service.DemonstratePrototype();
            return Success(result);
        }

        /// <summary>
        /// Demonstrates all creational patterns together
        /// </summary>
        [HttpGet("all")]
        public IActionResult DemonstrateAllCreationalPatterns()
        {
            var result = _service.DemonstrateAllCreationalPatterns();
            return Success(result);
        }

        /// <summary>
        /// Creates a custom product using Builder pattern
        /// </summary>
        [HttpPost("custom-product")]
        public IActionResult CreateCustomProduct([FromBody] CustomProductRequest request)
        {
            if (string.IsNullOrEmpty(request.Name))
                return ValidationError("Product name is required");

            if (request.Price <= 0)
                return ValidationError("Product price must be greater than 0");

            var result = _service.CreateCustomProduct(
                request.Name,
                request.Price,
                request.Description ?? "",
                request.Category ?? "General");
            
            return Success(result);
        }

        /// <summary>
        /// Get information about all creational patterns
        /// </summary>
        [HttpGet("info")]
        public IActionResult GetCreationalPatternsInfo()
        {
            var info = new
            {
                Title = "Creational Design Patterns",
                Description = "Creational design patterns are all about object creation mechanisms. They are used to create objects in a manner suitable for the situation, increasing flexibility and reusability.",
                Patterns = new[]
                {
                    new { Name = "Singleton", Description = "Ensures a class has only one instance", Endpoint = "/api/CreationalPatterns/singleton" },
                    new { Name = "Factory", Description = "Creates objects without specifying exact classes", Endpoint = "/api/CreationalPatterns/factory" },
                    new { Name = "Abstract Factory", Description = "Creates families of related objects", Endpoint = "/api/CreationalPatterns/abstract-factory" },
                    new { Name = "Builder", Description = "Constructs complex objects step by step", Endpoint = "/api/CreationalPatterns/builder" },
                    new { Name = "Prototype", Description = "Creates objects by cloning existing instances", Endpoint = "/api/CreationalPatterns/prototype" }
                },
                UseCases = new[]
                {
                    "Database connection management",
                    "UI component creation",
                    "Cross-platform object families",
                    "Complex configuration objects",
                    "Game object spawning"
                }
            };
            return Ok(info);
        }
    }

    public class CustomProductRequest
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
    }
}
