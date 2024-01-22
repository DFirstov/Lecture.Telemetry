var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Services.AddHttpClient();
var app = builder.Build();

app.MapGet("/products", (HttpClient httpClient) =>
{
    Console.WriteLine("GetProducts");
    return httpClient.GetFromJsonAsync<string[]>("http://stock:8080/products");
});

app.MapPost("/products/buy", async (string product, HttpClient httpClient) =>
{
    Console.WriteLine($"Buying {product}");

    var stockResult = await httpClient.PostAsync($"http://stock:8080/products/reserve?product={product}", null);
    Console.WriteLine($"Stock result: {stockResult.StatusCode}");

    var paymentResult = await httpClient.PostAsync($"http://payments:8080/pay?sum={Random.Shared.Next(100, 10000)}", null);
    Console.WriteLine($"Payment result: {paymentResult.StatusCode}");

    return Results.Ok();
});

app.Run();
