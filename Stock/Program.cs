var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

string[] products = ["milk", "bread", "cheese", "apples", "oranges", "bananas", "eggs", "chicken", "fish", "tomatoes"];

app.MapGet("/products", () => products)
    .WithName("GetProducts")
    .WithOpenApi();

app.MapPost("/products/reserve", (string product) =>
    {
        if (!products.Contains(product))
            return Results.BadRequest("Product not found");
        
        if (Random.Shared.Next(0, 10) == 0)
            return Results.BadRequest("Product out of stock");
        
        return Results.Ok();
    })
    .WithName("ReserveProduct")
    .WithOpenApi();

app.Run();
