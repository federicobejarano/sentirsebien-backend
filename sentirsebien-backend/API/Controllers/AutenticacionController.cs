using Microsoft.AspNetCore.Mvc;
using sentirsebien_backend.API.Dtos;
using sentirsebien_backend.Domain.Services;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class AutenticacionController : ControllerBase
{
    private readonly IAutenticacionService _autenticacionService;

    public AutenticacionController(IAutenticacionService autenticacionService)
    {
        _autenticacionService = autenticacionService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AutenticacionDTO dto)
    {
        // Validación del modelo
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Intentar autenticar al usuario
        bool esAutenticado = await _autenticacionService.AutenticarUsuario(dto.Email, dto.Contraseña); // error: `CS1061: bool no contiene una definición para GetAwaiter`

        if (!esAutenticado)
        {
            return Unauthorized("Credenciales incorrectas.");
        }

        // Generar y devolver token (suponiendo que la generación de token sea parte del servicio)
        var token = await _autenticacionService.GenerarToken(dto.Email); // error: `bool no contiene una definición para GetAwaiter`
        return Ok(new { Token = token });
    }
}

