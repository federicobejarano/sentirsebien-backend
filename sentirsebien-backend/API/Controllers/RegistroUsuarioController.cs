using Microsoft.AspNetCore.Mvc;
using sentirsebien_backend.API.Dtos;
using System.Threading.Tasks;

[ApiController]
[Route("api/usuarios")]
public class RegistroUsuarioController : ControllerBase
{
    // constructor para la inyección de dependencias (se podría usar un servicio de registro aquí)
    public RegistroUsuarioController() { }

    // Método POST para registrar a un nuevo usuario <-- solicitud `POST api/usuarios/registro`
    [HttpPost("registro")]
    public async Task<IActionResult> RegistrarUsuario([FromBody] RegistroUsuarioDTO usuarioDTO)
    {
        // validar los datos del DTO. Si los datos son inválidos, ModelState contiene los errores.
        if (!ModelState.IsValid)
        {
            // devolver código HTTP 400 con los errores de validación
            return BadRequest(ModelState);
        }

        // acá debería ir la lógica para verificar si el usuario
        // ya existe (usando el servicio de registro).
        // Por simplicidad asumimos que se registrará correctamente.

        // si todo es correcto, retornamos una respuesta 200 con un mensaje de éxito
        return Ok(new { mensaje = "Usuario registrado exitosamente." });
    }
}

