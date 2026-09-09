//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http;
//using System.Text.Json;
//using System.Threading.Tasks;

///*
//* README
//* An external API endpoint is been integrated to 'get list of users whose name has vowel(s)' and its details: https://jsonplaceholder.typicode.com/users
//* You need to find and fix the errors and make it work correctly.
//*/


//public class Program
//{
//    public static async Task Main(string[] args)
//    {
//        Console.WriteLine("================================");
//        Console.WriteLine("       USER API APPLICATION");
//        Console.WriteLine("================================");

//        RepositoryFactory factory = new RepositoryFactory();

//        IUserRepository repository = factory.Repository;

//        IUserService service = new UserService(repository);

//        Console.WriteLine();
//        Console.WriteLine("Fetching users...");

//        List<User> users = await service.GetUsersAsync();


//        if (users.Count == 0)
//        {
//            Console.WriteLine("No users found.");
//            return;
//        }

//        Console.WriteLine();
//        Console.WriteLine("================================");
//        Console.WriteLine("          ALL USERS");
//        Console.WriteLine("================================");

//        foreach (User user in users)
//        {
//            Console.WriteLine($"ID       : {user.Id}");
//            Console.WriteLine($"Name     : {user.Name}");
//            Console.WriteLine($"Username : {user.Username}");
//            Console.WriteLine($"Email    : {user.Email}");
//            Console.WriteLine($"City     : {user.Address.City}");
//            Console.WriteLine($"Company  : {user.Company.Name}");

//            Console.WriteLine("--------------------------------");
//        }


//        Console.WriteLine();
//        Console.Write("Enter User ID: ");

//        string? input = Console.ReadLine();

//        if (!int.TryParse(input, out int userId))
//        {
//            Console.WriteLine("Invalid User ID.");
//            return;
//        }


//        User? selectedUser = await service.GetUserByIdAsync(userId);
//        if (selectedUser == null)
//        {
//            Console.WriteLine($"User with ID {userId} not found.");
//            return;
//        }


//        Console.WriteLine();
//        Console.WriteLine("================================");
//        Console.WriteLine("       SELECTED USER");
//        Console.WriteLine("================================");

//        Console.WriteLine($"ID       : {selectedUser.Id}");
//        Console.WriteLine($"Name     : {selectedUser.Name}");
//        Console.WriteLine($"Username : {selectedUser.Username}");
//        Console.WriteLine($"Email    : {selectedUser.Email}");
//        Console.WriteLine($"Phone    : {selectedUser.Phone}");
//        Console.WriteLine($"Website  : {selectedUser.Website}");
//        Console.WriteLine($"City     : {selectedUser.Address.City}");
//        Console.WriteLine($"Company  : {selectedUser.Company.Name}");
//    }
//}

////MODELs
//public class User
//{
//    public int Id { get; set; }

//    public string Name { get; set; } = string.Empty;

//    public string Username { get; set; } = string.Empty;

//    public string Email { get; set; } = string.Empty;

//    public Address Address { get; set; } = new();

//    public string Phone { get; set; } = string.Empty;

//    public string Website { get; set; } = string.Empty;

//    public Company Company { get; set; } = new();
//}


//public class Address
//{
//    public string Street { get; set; } = string.Empty;

//    public string Suite { get; set; } = string.Empty;

//    public string City { get; set; } = string.Empty;

//    public string Zipcode { get; set; } = string.Empty;

//    public Geo Geo { get; set; } = new();
//}


//public class Geo
//{
//    public string Lat { get; set; } = string.Empty;

//    public string Lng { get; set; } = string.Empty;
//}


//public class Company
//{
//    public string Name { get; set; } = string.Empty;

//    public string CatchPhrase { get; set; } = string.Empty;

//    public string Bs { get; set; } = string.Empty;
//}

//public class RepositoryFactory
//{
//    public IUserRepository Repository { get; }

//    public RepositoryFactory()
//    {
//        // NOTE: no "using" here anymore - disposing the HttpClient in the
//        // constructor made every subsequent request fail with
//        // ObjectDisposedException.
//        var httpClient = new HttpClient();
//        httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
//        // Removed MaxResponseContentBufferSize = 1 (was truncating every response to 1 byte)
//        // Removed forced HTTP/3 (Version30 + RequestVersionExact) - the API doesn't support it
//        // Removed the bogus "Bearer null" Authorization header - not needed for this public API
//        Repository = new UserRepository(httpClient);
//    }
//}


////REPOSITORY INTERFACE
//public interface IUserRepository
//{
//    Task<List<User>> GetUsersAsync();
//}


////HTTP REPOSITORY
//public class UserRepository : IUserRepository
//{
//    private readonly HttpClient _httpClient;

//    public UserRepository(HttpClient httpClient)
//    {
//        _httpClient = httpClient;
//    }

//    public async Task<List<User>> GetUsersAsync()
//    {
//        try
//        {
//            // endpoint is "users", not "user"
//            string json = await _httpClient.GetStringAsync("users");
//            List<User>? users = JsonSerializer.Deserialize<List<User>>(json,
//                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

//            return users ?? new List<User>();
//        }
//        catch (HttpRequestException ex)
//        {
//            Console.WriteLine($"HTTP Error: {ex.Message}");

//            return new List<User>();
//        }
//    }
//}


////SERVICE INTERFACE
//public interface IUserService
//{
//    Task<List<User>> GetUsersAsync();

//    Task<User?> GetUserByIdAsync(int userId);
//}


////SERVICE
//public class UserService : IUserService
//{
//    private readonly IUserRepository _repository;

//    public UserService(IUserRepository repository)
//    {
//        _repository = repository;
//    }

//    public async Task<List<User>> GetUsersAsync()
//    {
//        return await _repository.GetUsersAsync();
//    }

//    public async Task<User?> GetUserByIdAsync(int userId)
//    {
//        List<User> users = await _repository.GetUsersAsync();

//        User? user = users.FirstOrDefault(x => x.Id == userId);

//        return user;
//    }
//}