using DesignPatterns.Domain.Interfaces.Structural;

namespace DesignPatterns.Domain.Patterns.Structural.Bridge
{
    // Bridge Pattern - Separates abstraction from implementation

    // Implementation side
    public class EmailSender : INotificationSender
    {
        public void SendNotification(string message)
        {
            Console.WriteLine($"Email: {message}");
        }
    }

    public class SmsSender : INotificationSender
    {
        public void SendNotification(string message)
        {
            Console.WriteLine($"SMS: {message}");
        }
    }

    public class PushNotificationSender : INotificationSender
    {
        public void SendNotification(string message)
        {
            Console.WriteLine($"Push: {message}");
        }
    }

    // Abstraction side
    public abstract class Notification
    {
        protected INotificationSender _sender;

        protected Notification(INotificationSender sender)
        {
            _sender = sender;
        }

        public abstract void Send(string message);
    }

    public class UrgentNotification : Notification
    {
        public UrgentNotification(INotificationSender sender) : base(sender) { }

        public override void Send(string message)
        {
            _sender.SendNotification($"URGENT: {message}");
        }
    }

    public class RegularNotification : Notification
    {
        public RegularNotification(INotificationSender sender) : base(sender) { }

        public override void Send(string message)
        {
            _sender.SendNotification($"Info: {message}");
        }
    }

    public class MarketingNotification : Notification
    {
        public MarketingNotification(INotificationSender sender) : base(sender) { }

        public override void Send(string message)
        {
            _sender.SendNotification($"Marketing: {message} - Don't miss out!");
        }
    }

    // Bridge Service
    public class NotificationService
    {
        public List<string> SendNotificationThroughAllChannels(string message, string type)
        {
            var results = new List<string>();
            var senders = new List<INotificationSender>
            {
                new EmailSender(),
                new SmsSender(),
                new PushNotificationSender()
            };

            foreach (var sender in senders)
            {
                Notification notification = type.ToLower() switch
                {
                    "urgent" => new UrgentNotification(sender),
                    "marketing" => new MarketingNotification(sender),
                    _ => new RegularNotification(sender)
                };

                notification.Send(message);
                results.Add($"{sender.GetType().Name} - {type} notification sent");
            }

            return results;
        }
    }
}