using ECommerceApp.DTOs.User;

namespace ECommerceApp.DTOs.Auth
{
    public class LoginResponseDto
    {
        public required UserResponseDto User { get; set; }
        public required string Token { get; set; }
    }
}