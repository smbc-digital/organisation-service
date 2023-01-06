var builder = WebApplication.CreateBuilder(args);

builder.Services
    .RegisterGateways(builder.Configuration)
    .RegisterServices()
    .RegisterProviders();

builder.Services.AddSwagger();
builder.Services.AddControllers();

builder.Services
    .AddHealthChecks()
    .AddCheck<TestHealthCheck>("TestHealthCheck");

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .WriteToElasticsearchAws(builder.Configuration)
    .CreateLogger());

var app = builder.Build();

if (!app.Environment.IsEnvironment("local") || !app.Environment.IsEnvironment("int"))
{
    app.UseExceptionHandler("/Error");
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("v1/swagger.json", "Street Service API");
});

app.UseAuthorization();
app.MapControllers();
app.UseHealthChecks("/healthcheck", HealthCheckConfig.Options);

app.Run();