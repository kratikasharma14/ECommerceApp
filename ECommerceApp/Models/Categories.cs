namespace ECommerceApp.Models
{
    public class Categories
    {
        public int Id { get; set; }
        public required  string Name { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public List<Product> Products { get; set; } = new List <Product>();

    }
}
