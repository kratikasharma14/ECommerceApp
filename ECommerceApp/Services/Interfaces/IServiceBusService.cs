namespace ECommerceApp.Services.Interfaces
{
    public interface IServiceBusService
    {
        Task SendMessageAsync<T>(T message);
    }
}
