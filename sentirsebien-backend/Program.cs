using sentirsebien_backend.API.Dtos;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodo", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()  // permitir POST, PUT, DELETE, GET
              .AllowAnyHeader();
    });
});

builder.Logging.ClearProviders();
builder.Logging.AddConsole();  // mostrar logs en la consola

var app = builder.Build();

app.Run();
