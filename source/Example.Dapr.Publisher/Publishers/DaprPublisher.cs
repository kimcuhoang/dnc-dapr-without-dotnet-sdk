using Example.Dapr.Publisher.Models;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Example.Dapr.Publisher.Publishers
{
    public class DaprPublisher
    {
        private readonly ILogger<DaprPublisher> _logger;
        private readonly HttpClient _httpClient;

        public DaprPublisher(ILogger<DaprPublisher> logger, HttpClient httpClient)
        {
            this._logger = logger;
            this._httpClient = httpClient;
        }

        public async Task Publish(ProductCreated request)
        {
            var jsonRequest = JsonSerializer.Serialize(request, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var requestStringContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            this._logger.LogInformation($"[{nameof(DaprPublisher)}] - Preparing publish: {jsonRequest}");

            this._logger.LogInformation($"[{nameof(DaprPublisher)}] - Publish Address: {this._httpClient.BaseAddress}");

            var response = await this._httpClient.PostAsync("/v1.0/publish/ProductCreated", requestStringContent);

            response.EnsureSuccessStatusCode();
        }
    }
}
