using ECommerceApp.Data;
using ECommerceApp.Models;
using ECommerceApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Categories>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Categories?> GetByIdAsync(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Categories category)
        {
            await _context.Categories.AddAsync(category);
        }

        public async Task UpdateAsync(Categories category)
        {
            _context.Categories.Update(category);
        }

        public async Task DeleteAsync(Categories category)
        {
            _context.Categories.Remove(category);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}