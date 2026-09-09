using ECommerceApp.DTOs.Cart;
using ECommerceApp.Models;
using ECommerceApp.Repositories.Interfaces;
using ECommerceApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public async Task<CartResponseDto> GetCartByUserIdAsync(int userId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);

            if (cart == null)
            {
                // agar cart hai hi nahi, khali cart response de do
                return new CartResponseDto
                {
                    CartId = 0,
                    UserId = userId,
                    Items = new List<CartItemResponseDto>(),
                    TotalAmount = 0
                };
            }

            return MapToCartResponseDto(cart);
        }

        public async Task<CartResponseDto> AddToCartAsync(AddToCartDto dto)
        {
            // 1. Product exist karta hai aur stock hai ya nahi check karo
            var product = await _cartRepository.GetProductByIdAsync(dto.ProductId);
            if (product == null)
                throw new ArgumentException("Product not found");

            if (product.Stock < dto.Quantity)
                throw new ArgumentException("Not enough stock available");

            // 2. User ka cart lao, nahi hai toh bana do
            var cart = await _cartRepository.GetCartByUserIdAsync(dto.UserId);
            if (cart == null)
            {
                cart = await _cartRepository.CreateCartAsync(dto.UserId);
            }

            // 3. Product already cart mein hai kya check karo
            var existingItem = await _cartRepository.GetCartItemAsync(cart.Id, dto.ProductId);

            if (existingItem != null)
            {
                // already hai toh quantity badhao
                existingItem.Quantity += dto.Quantity;
                await _cartRepository.UpdateCartItemAsync(existingItem);
            }
            else
            {
                // naya item add karo
                var newItem = new CartItem
                {
                    CartId = cart.Id,
                    ProductId = dto.ProductId,
                    Quantity = dto.Quantity,
                    UnitPrice = product.Price,
                    AddedDate = DateTime.Now
                };
                await _cartRepository.AddCartItemAsync(newItem);
            }

            await _cartRepository.SaveChangesAsync();

            var updatedCart = await _cartRepository.GetCartByUserIdAsync(dto.UserId);
            return MapToCartResponseDto(updatedCart!);
        }

        public async Task<CartResponseDto> IncreaseQuantityAsync(int cartItemId)
        {
            var item = await _cartRepository.GetCartItemByIdAsync(cartItemId);
            if (item == null)
                throw new ArgumentException("Cart item not found");

            if (item.Product.Stock <= item.Quantity)
                throw new ArgumentException("Not enough stock available");

            item.Quantity += 1;
            await _cartRepository.UpdateCartItemAsync(item);
            await _cartRepository.SaveChangesAsync();

            var cart = await _cartRepository.GetCartByUserIdAsync(item.Cart.UserId);
            return MapToCartResponseDto(cart!);
        }

        public async Task<CartResponseDto> DecreaseQuantityAsync(int cartItemId)
        {
            var item = await _cartRepository.GetCartItemByIdAsync(cartItemId);
            if (item == null)
                throw new ArgumentException("Cart item not found");

            var userId = item.Cart.UserId;

            if (item.Quantity <= 1)
            {
                // quantity 1 se 0 ho rahi hai, toh item hi remove kar do
                await _cartRepository.RemoveCartItemAsync(item);
            }
            else
            {
                item.Quantity -= 1;
                await _cartRepository.UpdateCartItemAsync(item);
            }

            await _cartRepository.SaveChangesAsync();

            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            return cart == null
                ? new CartResponseDto { CartId = 0, UserId = userId, Items = new List<CartItemResponseDto>(), TotalAmount = 0 }
                : MapToCartResponseDto(cart);
        }

        public async Task<CartResponseDto> RemoveItemAsync(int cartItemId)
        {
            var item = await _cartRepository.GetCartItemByIdAsync(cartItemId);
            if (item == null)
                throw new ArgumentException("Cart item not found");

            var userId = item.Cart.UserId;

            await _cartRepository.RemoveCartItemAsync(item);
            await _cartRepository.SaveChangesAsync();

            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            return cart == null
                ? new CartResponseDto { CartId = 0, UserId = userId, Items = new List<CartItemResponseDto>(), TotalAmount = 0 }
                : MapToCartResponseDto(cart);
        }

        // Helper method - Entity ko DTO mein convert karta hai (repeat hone se bacha)
        private CartResponseDto MapToCartResponseDto(Cart cart)
        {
            var items = cart.CartItem.Select(ci => new CartItemResponseDto
            {
                CartItemId = ci.Id,
                ProductId = ci.ProductId,
                ProductName = ci.Product.Name,
                UnitPrice = ci.UnitPrice,
                Quantity = ci.Quantity,
                Subtotal = ci.UnitPrice * ci.Quantity
            }).ToList();

            return new CartResponseDto
            {
                CartId = cart.Id,
                UserId = cart.UserId,
                Items = items,
                TotalAmount = items.Sum(i => i.Subtotal)
            };
        }
    }
}