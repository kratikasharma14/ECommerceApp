using ECommerceApp.Models;

namespace ECommerceApp.Repositories.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart?> GetCartByUserIdAsync(int userId);
        Task<Cart> CreateCartAsync(int userId);
        Task<CartItem?> GetCartItemAsync(int cartId, int productId);
        Task<CartItem?> GetCartItemByIdAsync(int cartItemId);
        Task AddCartItemAsync(CartItem cartItem);
        Task UpdateCartItemAsync(CartItem cartItem);
        Task RemoveCartItemAsync(CartItem cartItem);
        Task<Product?> GetProductByIdAsync(int productId);
        Task<bool> SaveChangesAsync();
    }
}