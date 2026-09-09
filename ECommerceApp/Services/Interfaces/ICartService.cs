using ECommerceApp.DTOs.Cart;

namespace ECommerceApp.Services.Interfaces
{
    public interface ICartService
    {
        Task<CartResponseDto> GetCartByUserIdAsync(int userId);
        Task<CartResponseDto> AddToCartAsync(AddToCartDto dto);
        Task<CartResponseDto> IncreaseQuantityAsync(int cartItemId);
        Task<CartResponseDto> DecreaseQuantityAsync(int cartItemId);
        Task<CartResponseDto> RemoveItemAsync(int cartItemId);
    }
}