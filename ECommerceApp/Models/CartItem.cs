namespace ECommerceApp.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        // Foreign Key - kis cart ka item hai
        public int CartId { get; set; }
        public Cart? Cart { get; set; }

        // Foreign Key - konsa product hai
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime AddedDate { get; set; } = DateTime.Now;
    }
}