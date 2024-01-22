var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
var app = builder.Build();

app.MapPost("/pay", (decimal sum) =>
{
    Console.WriteLine("Paying");
        
    var clientBalance = Random.Shared.Next(0, (int)(10 * sum));
    Console.WriteLine($"Client balance: {clientBalance}, sum: {sum}");
        
    if (clientBalance < sum)
    {
        return Results.BadRequest("Not enough money on the account");
    }

    return Results.Ok("Payment succeeded");
});

app.Run();
