using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Example.Dapr.Publisher.Publishers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Example.Dapr.Publisher
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNameCaseInsensitive = true);

            services
                .AddHttpClient<DaprPublisher>((provider, client) =>
                {
                    var logger = provider.GetRequiredService<ILogger<Startup>>();

                    var baseAddress = $"http://localhost:{Environment.GetEnvironmentVariable("DAPR_HTTP_PORT")}";

                    logger.LogInformation($"[{nameof(Startup)}] - Publish Address: {baseAddress}");

                    client.BaseAddress = new Uri(baseAddress, UriKind.Absolute);
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
