namespace ECommerceApp.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order? Order { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public string ProductName { get; set; } = string.Empty; // snapshot - product delete ho jaaye toh bhi naam pata rahe
        public decimal UnitPrice { get; set; }  // snapshot - price badal jaaye toh bhi purana price pata rahe
        public int Quantity { get; set; }
    }
}