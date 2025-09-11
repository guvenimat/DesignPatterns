namespace DesignPatterns.Domain.Entities.Common
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<Product> Products { get; set; } = new();
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"Order #{Id} - Total: ${TotalAmount} - Status: {Status}";
        }
    }
}