namespace ECommerceApp.DTOs.User
{
    public class UserResponseDto
    {
        public int Id { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required string Gender { get; set; }
        public required string Address { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}