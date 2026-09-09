using ECommerceApp.Models;

namespace ECommerceApp.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<Users>> GetAllAsync();
        Task<Users?> GetByIdAsync(int id);
        Task<Users?> GetByEmailAsync(string email);
        Task AddAsync(Users user);
        Task UpdateAsync(Users user);
        Task DeleteAsync(Users user);
        Task<bool> SaveChangesAsync();
    }
}