namespace ECommerceApp.DTOs.Order
{
    public class PlaceOrderDto
    {
        public int UserId { get; set; }
        public required string FullName { get; set; }
        public required string Phone { get; set; }
        public required string AddressLine { get; set; }
        public string PaymentMethod { get; set; } = "COD";
    }
}