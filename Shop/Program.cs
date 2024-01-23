using Prometheus;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Services.AddHttpClient();
var app = builder.Build();

app.UseMetricServer(url: "/metrics");
app.UseHttpMetrics();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("shop.log")
    .WriteTo.Seq("http://seq")
    .CreateLogger();

app.MapGet("/products", (HttpClient httpClient) =>
{
    Log.Information("GetProducts");
    return httpClient.GetFromJsonAsync<string[]>("http://stock:8080/products");
});

app.MapPost("/products/buy", async (string product, HttpClient httpClient) =>
{
    Log.Information("Buying {Product}", product);

    var stockResult = await httpClient.PostAsync($"http://stock:8080/products/reserve?product={product}", null);
    Log.Information("Stock result: {StatusCode}", stockResult.StatusCode);

    var paymentResult = await httpClient.PostAsync($"http://payments:8080/pay?sum={Random.Shared.Next(100, 10000)}", null);
    Log.Information("Payment result: {StatusCode}", paymentResult.StatusCode);

    return Results.Ok();
});

app.Run();
