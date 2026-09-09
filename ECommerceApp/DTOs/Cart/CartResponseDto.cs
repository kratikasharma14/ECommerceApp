namespace ECommerceApp.DTOs.Cart
{
    public class CartResponseDto
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public List<CartItemResponseDto> Items { get; set; } = new List<CartItemResponseDto>();
        public decimal TotalAmount { get; set; }
    }
}