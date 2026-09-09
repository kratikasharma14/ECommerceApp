using ECommerceApp.Models;

namespace ECommerceApp.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Categories>> GetAllAsync();
        Task<Categories?> GetByIdAsync(int id);
        Task AddAsync(Categories category);
        Task UpdateAsync(Categories category);
        Task DeleteAsync(Categories category);
        Task<bool> SaveChangesAsync();
    }
}