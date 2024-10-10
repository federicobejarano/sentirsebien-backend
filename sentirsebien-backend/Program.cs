using sentirsebien_backend.Dtos;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<Usuario> usuarios = [

        new (
            1,
            "Juancarlos",
            "G�mez",
            "jaun@carlos.com",
            "+5437-585477",
            "c�rdoba 4000",
            true
        ),
        new (
            2,
            "Mar�a",
            "Salazar",
            "salazar@gmail.com",
            "+5423-12323",
            "san juan 213",
            false
        ),
        new (
            3,
            "Vicente",
            "Cuevas",
            "vicente@yahoo.com",
            "+542342-4564",
            "la alameda 21",
            true
        )
];

app.MapGet("usuarios/{id}", (int id) => {
    return usuarios.Find(usuarios => usuarios.ID == id);
}).WithName("GetUsuario"); // nombre de la ruta

app.MapGet("/", () => "Conectado a servidor!");

app.Run();
