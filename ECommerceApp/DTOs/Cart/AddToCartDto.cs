namespace ECommerceApp.DTOs.Cart
{
    public class AddToCartDto
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; } = 1;
    }
}