var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/products", (HttpClient httpClient) => httpClient.GetFromJsonAsync<string[]>("http://stock:8080/products"))
    .WithName("GetProducts")
    .WithOpenApi();

app.MapPost("/products/buy", async (string product, HttpClient httpClient) =>
    {
        await httpClient.PostAsync($"http://stock:8080/products/reserve?product={product}", null);
        await httpClient.PostAsync($"http://payments:8080/pay?sum={Random.Shared.Next(100, 10000)}", null);
        return Results.Ok();
    })
    .WithName("BuyProduct")
    .WithOpenApi();

app.Run();
