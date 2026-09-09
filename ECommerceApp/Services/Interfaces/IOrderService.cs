using ECommerceApp.DTOs.Order;

namespace ECommerceApp.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResponseDto> PlaceOrderAsync(PlaceOrderDto dto);
        Task<List<OrderResponseDto>> GetOrdersByUserIdAsync(int userId);
    }
}