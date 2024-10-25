using Microsoft.AspNetCore.Builder; 
using Microsoft.Extensions.DependencyInjection; 
using Microsoft.Extensions.Hosting;
using sentirsebien_backend.API.Dtos;
using sentirsebien_backend.Domain.Services;
using sentirsebien_backend.Domain.Mappings;
using sentirsebien_backend.DataAccess.Repositories;
using sentirsebien_backend.DataAccess.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Text;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// registrar ApplicationDbContext con la cadena de conexión
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// registrar AutoMapper con el ensamblaje específico
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodo", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// habilitar controladores
builder.Services.AddControllers();

// registrar servicios
builder.Services.AddScoped<IRegistroUsuarioService, RegistroUsuarioService>();
builder.Services.AddScoped<IGestorRolesService, GestorRolesService>();
builder.Services.AddScoped<IValidacionService, ValidacionService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<IRolRepository, RolRepository>();

// configurar registro de logs
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build(); // line 48

app.Use(async (context, next) =>
{
    Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");
    await next.Invoke();
});


// aplicar política de CORS
app.UseCors("PermitirTodo");

// habilitar enrutamiento
app.UseRouting();

// mapear controladores
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();