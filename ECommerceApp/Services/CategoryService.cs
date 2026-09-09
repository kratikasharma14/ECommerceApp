using ECommerceApp.DTOs.Category;
using ECommerceApp.Models;
using ECommerceApp.Repositories.Interfaces;
using ECommerceApp.Services.Interfaces;

namespace ECommerceApp.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryResponseDto>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            return categories.Select(c => new CategoryResponseDto
            {
                Id = c.Id,
                Name = c.Name,
                CreatedDate = c.CreatedDate
            }).ToList();
        }

        public async Task<CategoryResponseDto?> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
                return null;

            return new CategoryResponseDto
            {
                Id = category.Id,
                Name = category.Name,
                CreatedDate = category.CreatedDate
            };
        }

        public async Task<CategoryResponseDto> CreateCategoryAsync(CreateCategoryDto dto)
        {
            var existing = await _categoryRepository.GetAllAsync();
            if (existing.Any(c => c.Name.ToLower() == dto.Name.ToLower()))
                throw new ArgumentException("Category with this name already exists");

            var category = new Categories
            {
                Name = dto.Name,
                CreatedDate = DateTime.Now
            };

            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangesAsync();

            return new CategoryResponseDto
            {
                Id = category.Id,
                Name = category.Name,
                CreatedDate = category.CreatedDate
            };
        }

        public async Task<bool> UpdateCategoryAsync(int id, UpdateCategoryDto dto)
        {
            var existing = await _categoryRepository.GetByIdAsync(id);
            if (existing == null)
                return false;

            existing.Name = dto.Name;

            await _categoryRepository.UpdateAsync(existing);
            return await _categoryRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var existing = await _categoryRepository.GetByIdAsync(id);
            if (existing == null)
                return false;

            await _categoryRepository.DeleteAsync(existing);
            return await _categoryRepository.SaveChangesAsync();
        }
    }
}