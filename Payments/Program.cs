using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Prometheus;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();

builder.Services
    .AddOpenTelemetry()
    .ConfigureResource(resource => resource.AddService("payments"))
    .WithTracing(tracing => tracing
        .AddAspNetCoreInstrumentation()
        .AddOtlpExporter(options => options.Endpoint = new Uri("http://jaeger:4317")));

var app = builder.Build();

app.UseMetricServer(url: "/metrics");
app.UseHttpMetrics();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("payments.log")
    .WriteTo.Seq("http://seq")
    .CreateLogger();

app.MapPost("/pay", (decimal sum) =>
{
    Log.Information("Paying");

    var clientBalance = Random.Shared.Next(0, (int)(10 * sum));
    Log.Information("Client balance: {ClientBalance}, sum: {Sum}", clientBalance, sum);

    if (clientBalance < sum)
    {
        return Results.BadRequest("Not enough money on the account");
    }

    return Results.Ok("Payment succeeded");
});

app.Run();
