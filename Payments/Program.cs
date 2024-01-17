var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/pay", (decimal sum) =>
    {
        var clientBalance = Random.Shared.Next(0, (int)(10 * sum));
        return clientBalance >= sum
            ? Results.Ok("Payment succeeded")
            : Results.BadRequest("Not enough money on the account");
    })
    .WithName("Pay")
    .WithOpenApi();

app.Run();
