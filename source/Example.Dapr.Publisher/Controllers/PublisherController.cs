using Example.Dapr.Publisher.Models;
using Example.Dapr.Publisher.Publishers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Threading.Tasks;

namespace Example.Dapr.Publisher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly ILogger<PublisherController> _logger;
        private readonly DaprPublisher _daprPublisher;

        public PublisherController(ILogger<PublisherController> logger, DaprPublisher daprPublisher)
        {
            this._logger = logger;
            this._daprPublisher = daprPublisher;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreated request)
        {
            var jsonRequest = JsonSerializer.Serialize(request, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            this._logger.LogInformation($"[{nameof(PublisherController)}] - Send to [{nameof(DaprPublisher)}]: {jsonRequest}");

            await this._daprPublisher.Publish(request);

            return Ok();
        }
    }
}