namespace ECommerceApp.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public Users? User { get; set; }

        // Delivery address - order ke saath hi save kar rahe hain
        public required string FullName { get; set; }
        public required string Phone { get; set; }
        public required string AddressLine { get; set; }

        public string PaymentMethod { get; set; } = "COD";

        public decimal TotalAmount { get; set; }

        public string Status { get; set; } = "Pending"; // Pending, Confirmed, Shipped, Delivered, Cancelled

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}