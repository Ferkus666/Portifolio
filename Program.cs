using KitchenRoutingSystem.Infrastructure;
using KitchenRoutingSystem.Services;
using KitchenRoutingSystem.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

/// controllers
builder.Services.AddControllers();

/// swagger build
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/// DI
builder.Services.AddSingleton<KitchenQueueStore>();
builder.Services.AddScoped<IKitchenRouterService, KitchenRouterService>();

var app = builder.Build();

/// swagger pipeline (NEEDS this to show the UI)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

///using https

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

public partial class Program { }


