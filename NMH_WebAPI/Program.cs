using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NMH_WebAPI;
using NMH_WebAPI.Helpers;
using NMH_WebAPI.HostedService;
using NMH_WebAPI.Messaging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();

// RabbitMQ
builder.Services.AddScoped<IProducer, Producer>();

// DI
builder.Services.AddSingleton<IProcessHelper, ProcessHelper>();

// DB
builder.Services.AddDbContext<Context>();

// Hosted service
builder.Services.AddHostedService<RabbitMqConsumerService>();

var app = builder.Build();

// Automatic migration on startup
using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<Context>().Database.Migrate();
}

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
