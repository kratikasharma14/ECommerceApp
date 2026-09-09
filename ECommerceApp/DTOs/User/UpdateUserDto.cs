namespace ECommerceApp.DTOs.User
{
    public class UpdateUserDto
    {
        public required string FullName { get; set; }
        public required string Phone { get; set; }
        public required string Gender { get; set; }
        public required string Address { get; set; }
    }
}