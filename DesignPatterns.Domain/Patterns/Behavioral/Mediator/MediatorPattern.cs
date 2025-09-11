using DesignPatterns.Domain.Interfaces.Common;

namespace DesignPatterns.Domain.Patterns.Behavioral.Mediator
{
    // Mediator Pattern - Define how set of objects interact

    // Chat Room Mediator Example
    public interface IChatMediator : IMediator
    {
        void AddUser(ChatUser user);
        void SendMessage(string message, ChatUser sender);
        void SendPrivateMessage(string message, ChatUser sender, ChatUser receiver);
        List<string> GetChatHistory();
        List<string> GetActiveUsers();
    }

    public class ChatRoom : IChatMediator
    {
        private readonly List<ChatUser> _users = new();
        private readonly List<string> _chatHistory = new();

        public void AddUser(ChatUser user)
        {
            _users.Add(user);
            _chatHistory.Add($"📢 {user.Name} joined the chat room");
            NotifyUserJoined(user);
        }

        public void SendMessage(string message, ChatUser sender)
        {
            var timestamp = DateTime.Now.ToString("HH:mm:ss");
            var formattedMessage = $"[{timestamp}] {sender.Name}: {message}";
            _chatHistory.Add(formattedMessage);
            
            // Notify all users except sender
            foreach (var user in _users.Where(u => u != sender))
            {
                user.ReceiveMessage(formattedMessage);
            }
        }

        public void SendPrivateMessage(string message, ChatUser sender, ChatUser receiver)
        {
            var timestamp = DateTime.Now.ToString("HH:mm:ss");
            var privateMessage = $"[{timestamp}] 🔒 {sender.Name} (private): {message}";
            receiver.ReceiveMessage(privateMessage);
            
            // Add to chat history with private indicator
            _chatHistory.Add($"[{timestamp}] 🔒 Private message from {sender.Name} to {receiver.Name}");
        }

        public void Notify(object sender, string @event)
        {
            if (sender is ChatUser user)
            {
                switch (@event)
                {
                    case "typing":
                        NotifyUserTyping(user);
                        break;
                    case "leave":
                        RemoveUser(user);
                        break;
                }
            }
        }

        public List<string> GetChatHistory() => new(_chatHistory);

        public List<string> GetActiveUsers() => _users.Select(u => u.Name).ToList();

        private void NotifyUserJoined(ChatUser newUser)
        {
            foreach (var user in _users.Where(u => u != newUser))
            {
                user.ReceiveMessage($"📢 {newUser.Name} joined the chat");
            }
        }

        private void NotifyUserTyping(ChatUser typingUser)
        {
            foreach (var user in _users.Where(u => u != typingUser))
            {
                user.ReceiveMessage($"💭 {typingUser.Name} is typing...");
            }
        }

        private void RemoveUser(ChatUser leavingUser)
        {
            _users.Remove(leavingUser);
            _chatHistory.Add($"📢 {leavingUser.Name} left the chat room");
            
            foreach (var user in _users)
            {
                user.ReceiveMessage($"📢 {leavingUser.Name} left the chat");
            }
        }
    }

    public abstract class ChatUser
    {
        protected IChatMediator _mediator;
        public string Name { get; protected set; }
        protected List<string> _messages = new();

        protected ChatUser(IChatMediator mediator, string name)
        {
            _mediator = mediator;
            Name = name;
        }

        public abstract void SendMessage(string message);
        public abstract void SendPrivateMessage(string message, ChatUser receiver);
        public abstract void ReceiveMessage(string message);
        public abstract void StartTyping();
        public abstract void LeaveChat();

        public List<string> GetMessages() => new(_messages);
    }

    public class RegularUser : ChatUser
    {
        public RegularUser(IChatMediator mediator, string name) : base(mediator, name) { }

        public override void SendMessage(string message)
        {
            Console.WriteLine($"{Name} sends: {message}");
            _mediator.SendMessage(message, this);
        }

        public override void SendPrivateMessage(string message, ChatUser receiver)
        {
            Console.WriteLine($"{Name} sends private message to {receiver.Name}: {message}");
            _mediator.SendPrivateMessage(message, this, receiver);
        }

        public override void ReceiveMessage(string message)
        {
            _messages.Add(message);
            Console.WriteLine($"{Name} received: {message}");
        }

        public override void StartTyping()
        {
            _mediator.Notify(this, "typing");
        }

        public override void LeaveChat()
        {
            _mediator.Notify(this, "leave");
        }
    }

    // Air Traffic Control Mediator Example
    public interface IAirTrafficControl : IMediator
    {
        void RequestLanding(Aircraft aircraft);
        void RequestTakeoff(Aircraft aircraft);
        void RequestRunwayChange(Aircraft aircraft, string newRunway);
        string GetControlTowerStatus();
    }

    public class ControlTower : IAirTrafficControl
    {
        private readonly Dictionary<string, bool> _runways = new();
        private readonly List<Aircraft> _aircraftInAirspace = new();
        private readonly List<string> _communications = new();

        public ControlTower()
        {
            // Initialize runways
            _runways["Runway-1"] = true; // true means available
            _runways["Runway-2"] = true;
            _runways["Runway-3"] = true;
        }

        public void RequestLanding(Aircraft aircraft)
        {
            var availableRunway = _runways.FirstOrDefault(r => r.Value).Key;
            
            if (availableRunway != null)
            {
                _runways[availableRunway] = false;
                aircraft.AssignRunway(availableRunway);
                var message = $"✈️ {aircraft.CallSign}: Landing approved on {availableRunway}";
                _communications.Add($"[{DateTime.Now:HH:mm:ss}] {message}");
                aircraft.ReceiveInstruction(message);
                
                if (!_aircraftInAirspace.Contains(aircraft))
                    _aircraftInAirspace.Add(aircraft);
            }
            else
            {
                var message = $"✈️ {aircraft.CallSign}: Hold position, all runways occupied";
                _communications.Add($"[{DateTime.Now:HH:mm:ss}] {message}");
                aircraft.ReceiveInstruction(message);
            }
        }

        public void RequestTakeoff(Aircraft aircraft)
        {
            if (aircraft.AssignedRunway != null && !_runways[aircraft.AssignedRunway])
            {
                var message = $"✈️ {aircraft.CallSign}: Takeoff approved from {aircraft.AssignedRunway}";
                _communications.Add($"[{DateTime.Now:HH:mm:ss}] {message}");
                aircraft.ReceiveInstruction(message);
                
                // Free the runway after takeoff
                _runways[aircraft.AssignedRunway] = true;
                aircraft.AssignRunway(null);
                _aircraftInAirspace.Remove(aircraft);
            }
            else
            {
                var message = $"✈️ {aircraft.CallSign}: Takeoff denied, runway not assigned or available";
                _communications.Add($"[{DateTime.Now:HH:mm:ss}] {message}");
                aircraft.ReceiveInstruction(message);
            }
        }

        public void RequestRunwayChange(Aircraft aircraft, string newRunway)
        {
            if (_runways.TryGetValue(newRunway, out var isAvailable) && isAvailable)
            {
                if (aircraft.AssignedRunway != null)
                {
                    _runways[aircraft.AssignedRunway] = true; // Free old runway
                }
                
                _runways[newRunway] = false; // Occupy new runway
                aircraft.AssignRunway(newRunway);
                
                var message = $"✈️ {aircraft.CallSign}: Runway change approved to {newRunway}";
                _communications.Add($"[{DateTime.Now:HH:mm:ss}] {message}");
                aircraft.ReceiveInstruction(message);
            }
            else
            {
                var message = $"✈️ {aircraft.CallSign}: Runway change denied, {newRunway} not available";
                _communications.Add($"[{DateTime.Now:HH:mm:ss}] {message}");
                aircraft.ReceiveInstruction(message);
            }
        }

        public void Notify(object sender, string @event)
        {
            if (sender is Aircraft aircraft)
            {
                var message = $"✈️ {aircraft.CallSign}: {@event}";
                _communications.Add($"[{DateTime.Now:HH:mm:ss}] {message}");
            }
        }

        public string GetControlTowerStatus()
        {
            var status = "=== Control Tower Status ===\n";
            status += "Runways:\n";
            foreach (var runway in _runways)
            {
                status += $"- {runway.Key}: {(runway.Value ? "Available" : "Occupied")}\n";
            }
            status += $"Aircraft in airspace: {_aircraftInAirspace.Count}\n";
            status += $"Active communications: {_communications.Count}";
            return status;
        }

        public List<string> GetCommunications() => new(_communications);
    }

    public class Aircraft
    {
        public string CallSign { get; private set; }
        public string? AssignedRunway { get; private set; }
        private readonly List<string> _receivedInstructions = new();

        public Aircraft(string callSign)
        {
            CallSign = callSign;
        }

        public void AssignRunway(string? runway)
        {
            AssignedRunway = runway;
        }

        public void ReceiveInstruction(string instruction)
        {
            _receivedInstructions.Add(instruction);
            Console.WriteLine($"📻 {CallSign} received: {instruction}");
        }

        public List<string> GetReceivedInstructions() => new(_receivedInstructions);
    }

    // Mediator Service
    public class MediatorService
    {
        public List<string> DemonstrateChatRoom()
        {
            var results = new List<string>();
            var chatRoom = new ChatRoom();

            // Create users
            var alice = new RegularUser(chatRoom, "Alice");
            var bob = new RegularUser(chatRoom, "Bob");
            var charlie = new RegularUser(chatRoom, "Charlie");

            using var stringWriter = new StringWriter();
            var originalOut = Console.Out;
            Console.SetOut(stringWriter);

            // Add users to chat room
            chatRoom.AddUser(alice);
            chatRoom.AddUser(bob);
            chatRoom.AddUser(charlie);

            // Send messages
            alice.SendMessage("Hello everyone!");
            bob.SendMessage("Hi Alice! How are you?");
            charlie.StartTyping();
            charlie.SendMessage("Good to see you both!");
            
            // Send private message
            alice.SendPrivateMessage("Can we talk later?", bob);
            
            // User leaves
            charlie.LeaveChat();
            
            bob.SendMessage("See you later Charlie!");

            Console.SetOut(originalOut);
            results.Add(stringWriter.ToString());

            results.Add("\n=== Chat History ===");
            results.AddRange(chatRoom.GetChatHistory());

            results.Add($"\n=== Active Users ===");
            results.AddRange(chatRoom.GetActiveUsers());

            return results;
        }

        public List<string> DemonstrateAirTrafficControl()
        {
            var results = new List<string>();
            var controlTower = new ControlTower();

            // Create aircraft
            var flight123 = new Aircraft("AA123");
            var flight456 = new Aircraft("BA456");
            var flight789 = new Aircraft("UA789");

            using var stringWriter = new StringWriter();
            var originalOut = Console.Out;
            Console.SetOut(stringWriter);

            // Simulate air traffic control
            Console.WriteLine("=== Air Traffic Control Simulation ===");
            
            controlTower.RequestLanding(flight123);
            controlTower.RequestLanding(flight456);
            controlTower.RequestLanding(flight789);
            
            // Try to takeoff
            controlTower.RequestTakeoff(flight123);
            
            // Request runway change
            controlTower.RequestRunwayChange(flight456, "Runway-1");
            
            // Another aircraft requests landing
            controlTower.RequestLanding(flight789);
            
            controlTower.RequestTakeoff(flight456);

            Console.SetOut(originalOut);
            results.Add(stringWriter.ToString());

            results.Add("\n" + controlTower.GetControlTowerStatus());

            results.Add("\n=== All Communications ===");
            results.AddRange(controlTower.GetCommunications());

            return results;
        }

        public List<string> CreateCustomChatScenario(List<string> userNames, List<(string sender, string message, string? receiver)> messages)
        {
            var results = new List<string>();
            var chatRoom = new ChatRoom();
            var users = new Dictionary<string, ChatUser>();

            using var stringWriter = new StringWriter();
            var originalOut = Console.Out;
            Console.SetOut(stringWriter);

            // Create and add users
            foreach (var userName in userNames)
            {
                var user = new RegularUser(chatRoom, userName);
                users[userName] = user;
                chatRoom.AddUser(user);
            }

            // Send messages
            foreach (var (senderName, message, receiverName) in messages)
            {
                if (users.TryGetValue(senderName, out var sender))
                {
                    if (receiverName != null && users.TryGetValue(receiverName, out var receiver))
                    {
                        sender.SendPrivateMessage(message, receiver);
                    }
                    else
                    {
                        sender.SendMessage(message);
                    }
                }
            }

            Console.SetOut(originalOut);
            results.Add(stringWriter.ToString());

            results.Add("\n=== Final Chat History ===");
            results.AddRange(chatRoom.GetChatHistory());

            return results;
        }
    }
}