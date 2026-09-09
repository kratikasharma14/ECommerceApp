using ECommerceApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceBusTestController : ControllerBase
    {
        private readonly IServiceBusService _serviceBusService;

        public ServiceBusTestController(IServiceBusService serviceBusService)
        {
            _serviceBusService = serviceBusService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage()
        {
            var message = new
            {
                OrderId = 101,
                Message = "First Azure Service Bus message",
                CreatedAt = DateTime.UtcNow
            };

            await _serviceBusService.SendMessageAsync(message);

            return Ok(new
            {
                message = "Message sent successfully"
            });
        }
    }
}