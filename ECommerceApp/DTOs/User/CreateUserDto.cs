namespace ECommerceApp.DTOs.User
{
    public class CreateUserDto
    {
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Phone { get; set; }
        public required string Gender { get; set; }
        public required string Address { get; set; }
    }
}