﻿﻿﻿﻿﻿using Microsoft.AspNetCore.Mvc;
using DesignPatterns.Application.Interfaces;

namespace DesignPatterns.Server.Controllers
{
    /// <summary>
    /// Controller for demonstrating behavioral design patterns
    /// </summary>
    [Route("api/[controller]")]
    public class BehavioralPatternsController : BaseController
    {
        private readonly IBehavioralPatternsService _service;

        public BehavioralPatternsController(IBehavioralPatternsService service)
        {
            _service = service;
        }

        /// <summary>
        /// Demonstrates the Observer pattern with news and stock systems
        /// </summary>
        [HttpGet("observer")]
        public IActionResult DemonstrateObserver()
        {
            var result = _service.DemonstrateObserver();
            return Success(result);
        }

        /// <summary>
        /// Demonstrates the Strategy pattern with discount and payment strategies
        /// </summary>
        [HttpGet("strategy")]
        public IActionResult DemonstrateStrategy()
        {
            var result = _service.DemonstrateStrategy();
            return Success(result);
        }

        /// <summary>
        /// Demonstrates the Command pattern with text editor and remote control
        /// </summary>
        [HttpGet("command")]
        public IActionResult DemonstrateCommand()
        {
            var result = _service.DemonstrateCommand();
            return Success(result);
        }

        /// <summary>
        /// Demonstrates the State pattern with order processing and traffic lights
        /// </summary>
        [HttpGet("state")]
        public IActionResult DemonstrateState()
        {
            var result = _service.DemonstrateState();
            return Success(result);
        }

        /// <summary>
        /// Demonstrates the Template Method pattern with order processing and data mining
        /// </summary>
        [HttpGet("template-method")]
        public IActionResult DemonstrateTemplateMethod()
        {
            var result = _service.DemonstrateTemplateMethod();
            return Success(result);
        }

        /// <summary>
        /// Demonstrates the Chain of Responsibility pattern with support and approval systems
        /// </summary>
        [HttpGet("chain-of-responsibility")]
        public IActionResult DemonstrateChainOfResponsibility()
        {
            var result = _service.DemonstrateChainOfResponsibility();
            return Success(result);
        }

        /// <summary>
        /// Demonstrates the Iterator pattern with collections and tree structures
        /// </summary>
        [HttpGet("iterator")]
        public IActionResult DemonstrateIterator()
        {
            var result = _service.DemonstrateIterator();
            return Success(result);
        }

        /// <summary>
        /// Demonstrates the Mediator pattern with chat rooms and air traffic control
        /// </summary>
        [HttpGet("mediator")]
        public IActionResult DemonstrateMediator()
        {
            var result = _service.DemonstrateMediator();
            return Success(result);
        }

        /// <summary>
        /// Demonstrates the Memento pattern with text editor and game save systems
        /// </summary>
        [HttpGet("memento")]
        public IActionResult DemonstrateMemento()
        {
            var result = _service.DemonstrateMemento();
            return Success(result);
        }

        /// <summary>
        /// Demonstrates the Visitor pattern with shape calculations and document analysis
        /// </summary>
        [HttpGet("visitor")]
        public IActionResult DemonstrateVisitor()
        {
            var result = _service.DemonstrateVisitor();
            return Success(result);
        }

        /// <summary>
        /// Demonstrates all behavioral patterns together
        /// </summary>
        [HttpGet("all")]
        public IActionResult DemonstrateAllBehavioralPatterns()
        {
            var result = _service.DemonstrateAllBehavioralPatterns();
            return Success(result);
        }

        /// <summary>
        /// Get a summary of all design patterns
        /// </summary>
        [HttpGet("summary")]
        public IActionResult GetPatternSummary()
        {
            var result = _service.GetPatternSummary();
            return Success(result);
        }

        /// <summary>
        /// Get information about all behavioral patterns
        /// </summary>
        [HttpGet("info")]
        public IActionResult GetBehavioralPatternsInfo()
        {
            var info = new
            {
                Title = "Behavioral Design Patterns",
                Description = "Behavioral design patterns are concerned with the communication and interaction between objects. They identify common communication patterns and help to define the responsibility of each object.",
                Patterns = new[]
                {
                    new { Name = "Observer", Description = "Defines one-to-many dependency between objects", Endpoint = "/api/BehavioralPatterns/observer" },
                    new { Name = "Strategy", Description = "Defines family of algorithms and makes them interchangeable", Endpoint = "/api/BehavioralPatterns/strategy" },
                    new { Name = "Command", Description = "Encapsulates requests as objects", Endpoint = "/api/BehavioralPatterns/command" },
                    new { Name = "State", Description = "Allows object to change behavior when internal state changes", Endpoint = "/api/BehavioralPatterns/state" },
                    new { Name = "Template Method", Description = "Defines skeleton of algorithm, lets subclasses override specific steps", Endpoint = "/api/BehavioralPatterns/template-method" },
                    new { Name = "Chain of Responsibility", Description = "Passes requests along chain of handlers", Endpoint = "/api/BehavioralPatterns/chain-of-responsibility" },
                    new { Name = "Iterator", Description = "Provides way to access elements sequentially", Endpoint = "/api/BehavioralPatterns/iterator" },
                    new { Name = "Mediator", Description = "Defines how set of objects interact", Endpoint = "/api/BehavioralPatterns/mediator" },
                    new { Name = "Memento", Description = "Captures and restores object state", Endpoint = "/api/BehavioralPatterns/memento" },
                    new { Name = "Visitor", Description = "Defines operations on object structure without changing classes", Endpoint = "/api/BehavioralPatterns/visitor" }
                },
                UseCases = new[]
                {
                    "Event handling systems",
                    "Algorithm selection",
                    "Undo/Redo operations",
                    "Workflow management",
                    "Data processing pipelines",
                    "Request routing",
                    "Collection traversal",
                    "Communication systems",
                    "State management",
                    "AST operations"
                }
            };
            return Ok(info);
        }
    }
}
