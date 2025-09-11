namespace DesignPatterns.Domain.Entities.Common
{
    public class ShoppingCart
    {
        public List<Product> Items { get; set; } = new();
        public decimal Total => Items.Sum(p => p.Price);

        public void AddItem(Product product)
        {
            Items.Add(product);
        }

        public void RemoveItem(Product product)
        {
            Items.Remove(product);
        }

        public void Clear()
        {
            Items.Clear();
        }
    }
}