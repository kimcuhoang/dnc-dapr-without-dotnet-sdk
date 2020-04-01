using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using CloudNative.CloudEvents;
using Example.Dapr.SecondSubscriber.Models;

namespace Example.Dapr.SecondSubscriber.Controllers
{
    [ApiController]
    public class SubscriberController : ControllerBase
    {
        private ILogger<SubscriberController> _logger;

        public SubscriberController(ILogger<SubscriberController> logger)
            => this._logger = logger;

        [HttpPost("ProductCreated")]
        public IActionResult SubscribeProductCreated(CloudEvent request)
        {
            this._logger.LogInformation($"[{nameof(SubscriberController)}]: Received request from publisher: {request.Data}");

            var productCreated = JsonSerializer.Deserialize<ProductCreated>(request.Data.ToString(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                AllowTrailingCommas = true
            });

            this._logger.LogInformation($"[{nameof(SubscriberController)}]: Deserialized to {nameof(ProductCreated)}: {JsonSerializer.Serialize(productCreated)}");

            return Ok();
        }
    }
}