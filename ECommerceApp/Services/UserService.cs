using ECommerceApp.DTOs.User;
using ECommerceApp.Models;
using ECommerceApp.Repositories.Interfaces;
using ECommerceApp.Services.Interfaces;

namespace ECommerceApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserResponseDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();

            return users.Select(u => new UserResponseDto
            {
                Id = u.Id,
                FullName = u.FullName,
                Email = u.Email,
                Phone = u.Phone,
                Gender = u.Gender,
                Address = u.Address,
                CreatedDate = u.CreatedDate
            }).ToList();
        }

        public async Task<UserResponseDto?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return null;

            return new UserResponseDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Phone = user.Phone,
                Gender = user.Gender,
                Address = user.Address,
                CreatedDate = user.CreatedDate
            };
        }

        public async Task<UserResponseDto> CreateUserAsync(CreateUserDto dto)
        {
            var existing = await _userRepository.GetByEmailAsync(dto.Email);
            if (existing != null)
                throw new ArgumentException("Email already registered");

            var user = new Users
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Phone = dto.Phone,
                Gender = dto.Gender,
                Address = dto.Address,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),   // ✅ yeh line add karo
                CreatedDate = DateTime.Now
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return new UserResponseDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Phone = user.Phone,
                Gender = user.Gender,
                Address = user.Address,
                CreatedDate = user.CreatedDate
            };
        }
        public async Task<bool> UpdateUserAsync(int id, UpdateUserDto dto)
        {
            var existing = await _userRepository.GetByIdAsync(id);
            if (existing == null)
                return false;

            existing.FullName = dto.FullName;
            existing.Phone = dto.Phone;
            existing.Gender = dto.Gender;
            existing.Address = dto.Address;

            await _userRepository.UpdateAsync(existing);
            return await _userRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var existing = await _userRepository.GetByIdAsync(id);
            if (existing == null)
                return false;

            await _userRepository.DeleteAsync(existing);
            return await _userRepository.SaveChangesAsync();
        }
    }
}