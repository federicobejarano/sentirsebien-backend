using Microsoft.AspNetCore.Mvc;
using sentirsebien_backend.API.Dtos;
using sentirsebien_backend.Domain.Services;
using System.Threading.Tasks;

[ApiController]
[Route("api/usuarios")]
public class RegistroUsuarioController : ControllerBase
{
    private readonly IRegistroUsuarioService _registroUsuarioService;
    
    // constructor para la inyección de dependencias (se podría usar un servicio de registro aquí)
    public RegistroUsuarioController(IRegistroUsuarioService registroUsuarioService)
    {
        _registroUsuarioService = registroUsuarioService;
    }

    // método POST para registrar a un nuevo usuario <-- solicitud `POST api/usuarios/registro`
    [HttpPost("registro")]
    public async Task<IActionResult> RegistrarUsuario([FromBody] RegistroUsuarioDTO usuarioDTO)
    {
        // validar los datos del DTO. Si los datos son inválidos, ModelState contiene los errores.
        if (!ModelState.IsValid)
        {
            // devolver código HTTP 400 con los errores de validación
            return BadRequest(ModelState);
        }

        // lógica para verificar si el usuario ya existe (usando el servicio de registro).
        var result = await _registroUsuarioService.RegistrarUsuario(usuarioDTO);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);  // mensaje de error personalizado
        }

        // si todo es correcto, retornar respuesta 200 con mensaje de éxito
        return Ok(new { mensaje = "Usuario registrado exitosamente." });
    }
}

