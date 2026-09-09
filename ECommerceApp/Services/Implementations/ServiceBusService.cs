using System.Text.Json;
using Azure.Messaging.ServiceBus;
using ECommerceApp.Services.Interfaces;

namespace ECommerceApp.Services.Implementations
{
    public class ServiceBusService : IServiceBusService
    {
        private readonly ServiceBusSender _sender;

        public ServiceBusService(
            IConfiguration configuration)
        {
            var connectionString =
                configuration["AzureServiceBus:ConnectionString"];

            var queueName =
                configuration["AzureServiceBus:QueueName"];

            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException(
                    "Azure Service Bus connection string is missing.");

            if (string.IsNullOrEmpty(queueName))
                throw new InvalidOperationException(
                    "Azure Service Bus queue name is missing.");

            var client = new ServiceBusClient(connectionString);

            _sender = client.CreateSender(queueName);
        }

        public async Task SendMessageAsync<T>(T message)
        {
            var jsonMessage = JsonSerializer.Serialize(message);

            var serviceBusMessage = new ServiceBusMessage(jsonMessage)
            {
                ContentType = "application/json"
            };

            await _sender.SendMessageAsync(serviceBusMessage);
        }
    }
}
