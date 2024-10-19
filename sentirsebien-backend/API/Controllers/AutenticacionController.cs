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
        // validar modelo
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // autenticar al usuario
        bool esAutenticado = await _autenticacionService.AutenticarUsuario(dto.Email, dto.Contraseña);

        if (!esAutenticado)
        {
            return Unauthorized("Credenciales incorrectas.");
        }

        // generar y devolver token
        var token = await _autenticacionService.GenerarToken(dto.Email);
        return Ok(new { Token = token });
    }
}

