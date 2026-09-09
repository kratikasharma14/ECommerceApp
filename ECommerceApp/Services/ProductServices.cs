using ECommerceApp.DTOs.Product;
using ECommerceApp.Models;
using ECommerceApp.Repositories.Interfaces;
using ECommerceApp.Services.Interfaces;

namespace ECommerceApp.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<ProductResponseDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();

            return products.Select(p => new ProductResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                ImageUrl = p.ImageUrl,
                CategoryId = p.CategoryId,
                CreatedDate = p.CreatedDate,
                UpdatedDate = p.UpdatedDate
            }).ToList();
        }

        public async Task<ProductResponseDto?> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return null;

            return new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                ImageUrl = product.ImageUrl,
                CategoryId = product.CategoryId,
                CreatedDate = product.CreatedDate,
                UpdatedDate = product.UpdatedDate
            };
        }

        public async Task<ProductResponseDto> CreateProductAsync(CreateProductDto dto)
        {
            if (dto.Price <= 0)
                throw new ArgumentException("Price must be greater than 0");

            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                Stock = dto.Stock,
                ImageUrl = dto.ImageUrl,
                CategoryId = dto.CategoryId,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();

            return new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                ImageUrl = product.ImageUrl,   // ✅ ADD THIS
                CategoryId = product.CategoryId,
                CreatedDate = product.CreatedDate,
                UpdatedDate = product.UpdatedDate
            };
        }
        public async Task<bool> UpdateProductAsync(int id, UpdateProductDto dto)
        {
            var existing = await _productRepository.GetByIdAsync(id);
            if (existing == null)
                return false;

            existing.Name = dto.Name;
            existing.Price = dto.Price;
            existing.Stock = dto.Stock;
            existing.ImageUrl = dto.ImageUrl;
            existing.CategoryId = dto.CategoryId;
            existing.UpdatedDate = DateTime.Now;

            await _productRepository.UpdateAsync(existing);
            return await _productRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var existing = await _productRepository.GetByIdAsync(id);
            if (existing == null)
                return false;

            await _productRepository.DeleteAsync(existing);
            return await _productRepository.SaveChangesAsync();
        }
    }
}