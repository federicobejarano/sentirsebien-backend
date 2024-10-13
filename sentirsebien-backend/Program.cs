using sentirsebien_backend.API.Dtos;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodo", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()  // Permitir POST, PUT, DELETE, GET
              .AllowAnyHeader();
    });
});

builder.Logging.ClearProviders();
builder.Logging.AddConsole();  // Mostrar logs en la consola

var app = builder.Build();

List<GetUsuarioDto> usuarios = new List<GetUsuarioDto>
{
    new (1, "Juancarlos", "Gómez", "jaun@carlos.com", "+5437-585477", "córdoba 4000", true),
    new (2, "María", "Salazar", "salazar@gmail.com", "+5423-12323", "san juan 213", false),
    new (3, "Vicente", "Cuevas", "vicente@yahoo.com", "+542342-4564", "la alameda 21", true)
};

// Configurar rutas GET

app.MapGet("usuarios/{id}", (int id) => {
    return usuarios.Find(usuarios => usuarios.ID == id);
}).WithName("GetUsuario");

// Ruta para comprobar conexión
app.MapGet("/", () => "Conectado a servidor!");

// Configurar rutas POST

app.MapPost("usuarios", (CreateUsuarioDto newUsuario) => {
    int id = usuarios.Count + 1; // reemplazar por ID generado por la BD
    GetUsuarioDto usuario = new(id, newUsuario.Nombre, newUsuario.Apellido, newUsuario.Email, newUsuario.Telefono, newUsuario.Direccion, newUsuario.EsCliente);

    usuarios.Add(usuario);

    // Corregir el nombre de la ruta
    return Results.CreatedAtRoute("GetUsuario", new { id = id }, usuario);
});

// Configurar rutas PUT

app.MapPut("usuarios/{id}", (int id, CreateUsuarioDto usuarioActualizado) => {
    GetUsuarioDto? usuarioActual = usuarios.Find(usuario => usuario.ID == id);
    if (usuarioActual == null)
    {
        return Results.NotFound();
    }

    // Crear un nuevo objeto de usuario con los valores actualizados
    usuarioActual = new GetUsuarioDto(
        id,
        usuarioActualizado.Nombre,
        usuarioActualizado.Apellido,
        usuarioActualizado.Email,
        usuarioActualizado.Telefono,
        usuarioActualizado.Direccion,
        usuarioActualizado.EsCliente
    );

    int index = usuarios.FindIndex(usuario => usuario.ID == id);
    if (index >= 0)
    {
        usuarios[index] = usuarioActual;
    }

    return Results.Ok();
});

// Configurar rutas DELETE

app.MapDelete("usuarios/{id}", (int id) => {
    int idUsuario = usuarios.FindIndex(usuario => usuario.ID == id);
    if (idUsuario == -1)
    {
        return Results.NoContent();
    }
    usuarios.RemoveAt(id - 1);
    return Results.NoContent();
});



app.Run();
