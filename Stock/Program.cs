using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
var app = builder.Build();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("stock.log")
    .WriteTo.Seq("http://seq")
    .CreateLogger();

string[] products = ["milk", "bread", "cheese", "apples", "oranges", "bananas", "eggs", "chicken", "fish", "tomatoes"];

app.MapGet("/products", () =>
{
    Log.Information("GetProducts");
    return products;
});

app.MapPost("/products/reserve", (string product) =>
{
    Log.Information("ReserveProduct {Product}", product);

    if (!products.Contains(product))
        return Results.BadRequest("Product not found");

    if (Random.Shared.Next(0, 10) == 0)
        return Results.BadRequest("Product out of stock");

    return Results.Ok();
});

app.Run();
