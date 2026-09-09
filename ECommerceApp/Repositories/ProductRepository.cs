using ECommerceApp.Data;
using ECommerceApp.Models;
using ECommerceApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

            public async Task UpdateAsync(Product product)
            {
                _context.Products.Update(product);
            }

            public async Task DeleteAsync(Product product)
            {
                _context.Products.Remove(product);
            }

            public async Task<bool> SaveChangesAsync()
            {
                return await _context.SaveChangesAsync() > 0;
            }
        }
    }