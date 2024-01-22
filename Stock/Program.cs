var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
var app = builder.Build();

string[] products = ["milk", "bread", "cheese", "apples", "oranges", "bananas", "eggs", "chicken", "fish", "tomatoes"];

app.MapGet("/products", () =>
{
    return products;
});

app.MapPost("/products/reserve", (string product) =>
{
    if (!products.Contains(product))
        return Results.BadRequest("Product not found");

    if (Random.Shared.Next(0, 10) == 0)
        return Results.BadRequest("Product out of stock");

    return Results.Ok();
});

app.Run();
