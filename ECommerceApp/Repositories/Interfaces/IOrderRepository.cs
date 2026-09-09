using ECommerceApp.Models;

namespace ECommerceApp.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task AddOrderAsync(Order order);
        Task<Order?> GetOrderByIdAsync(int orderId);
        Task<List<Order>> GetOrdersByUserIdAsync(int userId);
        Task<bool> SaveChangesAsync();
    }
}