namespace ECommerceApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? ImageUrl { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        public int CategoryId { get; set; }
        public Categories? Category { get; set; }
    }
}