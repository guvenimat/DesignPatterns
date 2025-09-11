using DesignPatterns.Domain.Interfaces.Common;

namespace DesignPatterns.Domain.Patterns.Behavioral.ChainOfResponsibility
{
    // Chain of Responsibility Pattern - Pass requests along chain of handlers

    // Base Handler
    public abstract class SupportHandler : IHandler
    {
        private IHandler? _nextHandler;

        public IHandler SetNext(IHandler handler)
        {
            _nextHandler = handler;
            return handler;
        }

        public virtual object Handle(object request)
        {
            if (_nextHandler != null)
            {
                return _nextHandler.Handle(request);
            }
            
            return "Request could not be handled";
        }
    }

    // Request types
    public class SupportRequest
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Priority { get; set; } // 1-5, 5 being highest
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public override string ToString()
        {
            return $"Request #{Id}: {Type} - Priority {Priority} - {Description}";
        }
    }

    // Concrete Handlers
    public class Level1SupportHandler : SupportHandler
    {
        public override object Handle(object request)
        {
            if (request is SupportRequest supportRequest)
            {
                if (supportRequest.Type.ToLower() == "basic" && supportRequest.Priority <= 2)
                {
                    return $"🎧 Level 1 Support handled: {supportRequest}\n" +
                           "- Password reset completed\n" +
                           "- Basic troubleshooting provided\n" +
                           "- Resolution time: 15 minutes";
                }
            }

            return base.Handle(request);
        }
    }

    public class Level2SupportHandler : SupportHandler
    {
        public override object Handle(object request)
        {
            if (request is SupportRequest supportRequest)
            {
                if ((supportRequest.Type.ToLower() == "technical" && supportRequest.Priority <= 3) ||
                    (supportRequest.Type.ToLower() == "billing" && supportRequest.Priority <= 4))
                {
                    return $"🔧 Level 2 Support handled: {supportRequest}\n" +
                           "- Technical diagnosis completed\n" +
                           "- System configuration adjusted\n" +
                           "- Resolution time: 45 minutes";
                }
            }

            return base.Handle(request);
        }
    }

    public class Level3SupportHandler : SupportHandler
    {
        public override object Handle(object request)
        {
            if (request is SupportRequest supportRequest)
            {
                if (supportRequest.Type.ToLower() == "critical" || supportRequest.Priority >= 4)
                {
                    return $"🚨 Level 3 Support handled: {supportRequest}\n" +
                           "- Critical issue escalation\n" +
                           "- Senior engineer assigned\n" +
                           "- Emergency protocols activated\n" +
                           "- Resolution time: 2 hours";
                }
            }

            return base.Handle(request);
        }
    }

    public class ManagerHandler : SupportHandler
    {
        public override object Handle(object request)
        {
            if (request is SupportRequest supportRequest)
            {
                return $"👔 Manager handled: {supportRequest}\n" +
                       "- Executive escalation\n" +
                       "- Customer retention protocols\n" +
                       "- Personal attention provided\n" +
                       "- Resolution time: 24 hours";
            }

            return base.Handle(request);
        }
    }

    // Expense Approval Chain Example
    public class ExpenseRequest
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;
        public DateTime RequestDate { get; set; } = DateTime.UtcNow;

        public override string ToString()
        {
            return $"Expense #{Id}: {EmployeeName} - ${Amount:F2} for {Description}";
        }
    }

    public abstract class ApprovalHandler : IHandler
    {
        private IHandler? _nextHandler;

        public IHandler SetNext(IHandler handler)
        {
            _nextHandler = handler;
            return handler;
        }

        public virtual object Handle(object request)
        {
            if (_nextHandler != null)
            {
                return _nextHandler.Handle(request);
            }
            
            return "Expense request denied - exceeds all approval limits";
        }
    }

    public class TeamLeaderHandler : ApprovalHandler
    {
        public override object Handle(object request)
        {
            if (request is ExpenseRequest expenseRequest && expenseRequest.Amount <= 500)
            {
                return $"✅ Team Leader approved: {expenseRequest}\n" +
                       "- Approval limit: $500\n" +
                       "- Approved for immediate reimbursement";
            }

            return base.Handle(request);
        }
    }

    public class DepartmentManagerHandler : ApprovalHandler
    {
        public override object Handle(object request)
        {
            if (request is ExpenseRequest expenseRequest && expenseRequest.Amount <= 2000)
            {
                return $"✅ Department Manager approved: {expenseRequest}\n" +
                       "- Approval limit: $2,000\n" +
                       "- Requires receipt submission";
            }

            return base.Handle(request);
        }
    }

    public class DirectorHandler : ApprovalHandler
    {
        public override object Handle(object request)
        {
            if (request is ExpenseRequest expenseRequest && expenseRequest.Amount <= 10000)
            {
                return $"✅ Director approved: {expenseRequest}\n" +
                       "- Approval limit: $10,000\n" +
                       "- Requires detailed justification";
            }

            return base.Handle(request);
        }
    }

    public class CEOHandler : ApprovalHandler
    {
        public override object Handle(object request)
        {
            if (request is ExpenseRequest expenseRequest)
            {
                return $"✅ CEO approved: {expenseRequest}\n" +
                       "- Executive approval\n" +
                       "- Board notification required for amounts over $50,000";
            }

            return base.Handle(request);
        }
    }

    // Chain of Responsibility Service
    public class ChainOfResponsibilityService
    {
        public List<string> DemonstrateSupportChain()
        {
            var results = new List<string>();

            // Set up the chain
            var level1 = new Level1SupportHandler();
            var level2 = new Level2SupportHandler();
            var level3 = new Level3SupportHandler();
            var manager = new ManagerHandler();

            level1.SetNext(level2).SetNext(level3).SetNext(manager);

            // Create test requests
            var requests = new List<SupportRequest>
            {
                new() { Id = 1, Type = "basic", Priority = 1, Description = "Password reset needed" },
                new() { Id = 2, Type = "technical", Priority = 3, Description = "Software installation issue" },
                new() { Id = 3, Type = "critical", Priority = 5, Description = "Server is down" },
                new() { Id = 4, Type = "billing", Priority = 2, Description = "Invoice discrepancy" },
                new() { Id = 5, Type = "unknown", Priority = 4, Description = "Unusual system behavior" }
            };

            results.Add("=== Support Ticket Chain Demo ===\n");

            foreach (var request in requests)
            {
                var response = level1.Handle(request);
                results.Add($"Request: {request}");
                results.Add($"Response: {response}\n");
            }

            return results;
        }

        public List<string> DemonstrateExpenseApprovalChain()
        {
            var results = new List<string>();

            // Set up the approval chain
            var teamLeader = new TeamLeaderHandler();
            var manager = new DepartmentManagerHandler();
            var director = new DirectorHandler();
            var ceo = new CEOHandler();

            teamLeader.SetNext(manager).SetNext(director).SetNext(ceo);

            // Create test expense requests
            var expenses = new List<ExpenseRequest>
            {
                new() { Id = 1, Amount = 250m, Description = "Office supplies", EmployeeName = "John Doe" },
                new() { Id = 2, Amount = 1500m, Description = "Conference registration", EmployeeName = "Jane Smith" },
                new() { Id = 3, Amount = 5000m, Description = "New equipment", EmployeeName = "Bob Wilson" },
                new() { Id = 4, Amount = 25000m, Description = "Office renovation", EmployeeName = "Alice Brown" },
                new() { Id = 5, Amount = 75000m, Description = "Company vehicle fleet", EmployeeName = "Mike Johnson" }
            };

            results.Add("=== Expense Approval Chain Demo ===\n");

            foreach (var expense in expenses)
            {
                var response = teamLeader.Handle(expense);
                results.Add($"Request: {expense}");
                results.Add($"Response: {response}\n");
            }

            return results;
        }

        public string ProcessCustomSupportRequest(string type, int priority, string description)
        {
            var level1 = new Level1SupportHandler();
            var level2 = new Level2SupportHandler();
            var level3 = new Level3SupportHandler();
            var manager = new ManagerHandler();

            level1.SetNext(level2).SetNext(level3).SetNext(manager);

            var request = new SupportRequest
            {
                Id = new Random().Next(1000, 9999),
                Type = type,
                Priority = priority,
                Description = description
            };

            var response = level1.Handle(request);
            return $"Request: {request}\nResponse: {response}";
        }

        public string ProcessCustomExpenseRequest(decimal amount, string description, string employeeName)
        {
            var teamLeader = new TeamLeaderHandler();
            var manager = new DepartmentManagerHandler();
            var director = new DirectorHandler();
            var ceo = new CEOHandler();

            teamLeader.SetNext(manager).SetNext(director).SetNext(ceo);

            var request = new ExpenseRequest
            {
                Id = new Random().Next(1000, 9999),
                Amount = amount,
                Description = description,
                EmployeeName = employeeName
            };

            var response = teamLeader.Handle(request);
            return $"Request: {request}\nResponse: {response}";
        }
    }
}