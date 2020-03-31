using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Example.Dapr.SecondSubscriber.Controllers
{
    [ApiController]
    public class SubscriberController : ControllerBase
    {
        private ILogger<SubscriberController> _logger;

        public SubscriberController(ILogger<SubscriberController> logger)
            => this._logger = logger;

        [HttpPost("ProductCreated")]
        public IActionResult SubscribeProductCreated(object request)
        {
            var requestJson = JsonSerializer.Serialize(request, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            this._logger.LogInformation($"[{nameof(SubscriberController)}]: Received request from publisher: {requestJson}");

            return Ok();
        }
    }
}