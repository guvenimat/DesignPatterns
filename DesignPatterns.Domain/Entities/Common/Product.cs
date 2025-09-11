namespace DesignPatterns.Domain.Entities.Common
{
    /// <summary>
    /// Simple product class for design pattern demonstrations
    /// </summary>
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        
        public override string ToString()
        {
            return $"Product: {Name} - ${Price} ({Category})";
        }

        public Product Clone()
        {
            return new Product
            {
                Id = this.Id,
                Name = this.Name,
                Price = this.Price,
                Description = this.Description,
                Category = this.Category
            };
        }
    }
}