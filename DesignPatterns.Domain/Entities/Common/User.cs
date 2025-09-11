namespace DesignPatterns.Domain.Entities.Common
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        public override string ToString()
        {
            return $"User: {Username} ({Email}) - Role: {Role}";
        }
    }
}