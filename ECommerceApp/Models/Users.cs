namespace ECommerceApp.Models
{
    public class Users
    {
        public int Id { get; set; }
        public required string FullName { get; set; }
        public required string Email{  get; set; }
        public required string PasswordHash { get; set; }   
        public required string Phone {  get; set; }
        public required string Gender {  get; set; }
        public required string Address {  get; set; }
        public DateTime CreatedDate { get; set; }
        public Cart? Cart { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();


    }
}
