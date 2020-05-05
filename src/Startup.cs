using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using organisation_service.Providers;
using organisation_service.Utils.HealthChecks;
using organisation_service.Utils.ServiceCollectionExtensions;
using StockportGovUK.AspNetCore.Availability;
using StockportGovUK.AspNetCore.Availability.Middleware;
using StockportGovUK.AspNetCore.Middleware;
using StockportGovUK.NetStandard.Gateways;
using System.Diagnostics.CodeAnalysis;

namespace organisation_service
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<IOrganisationProvider, FakeOrganisationProvider>();
            services.AddSingleton<IOrganisationProvider, VerintOrganisationProvider>();
            services.AddSwagger();
            services.AddAvailability();
            services.AddResilientHttpClients<IGateway, Gateway>(Configuration);
            services.AddHealthChecks()
                            .AddCheck<TestHealthCheck>("TestHealthCheck");

            services.RegisterServices();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsEnvironment("local"))
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseMiddleware<Availability>();
            app.UseMiddleware<ApiExceptionHandling>();
            app.UseHttpsRedirection();

            app.UseHealthChecks("/healthcheck", HealthCheckConfig.Options);
          
            app.UseSwagger();        
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"{(env.IsEnvironment("local") ? string.Empty : "/organisationservice")}/swagger/v1/swagger.json", "Organisation service API");
            });
        }
    }
}
