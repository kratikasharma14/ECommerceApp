namespace ECommerceApp.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime ModifiedDate { get; set; } = DateTime.Now;

        public Users? User { get; set; }

        public List<CartItem> CartItem { get; set; } = new List<CartItem>();
    }
}