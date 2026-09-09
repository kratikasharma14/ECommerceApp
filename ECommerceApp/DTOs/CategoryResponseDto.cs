namespace ECommerceApp.DTOs.Category
{
    public class CategoryResponseDto
    {   
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}