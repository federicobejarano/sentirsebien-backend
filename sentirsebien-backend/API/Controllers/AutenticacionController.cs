using Microsoft.AspNetCore.Mvc;
using sentirsebien_backend.API.Dtos;
using sentirsebien_backend.Domain.Services;
using System.Threading.Tasks;


/*
    PROBAR EN POSTMAN:

    - solicitud:

    POST http://localhost:[número de localhost]/api/autenticacion/login

    - cuerpo:

    {
      "Email": "juan.perez.2@example.com",
      "Contraseña": "password123"
    }
 
*/

[ApiController]
[Route("api/[controller]")]
public class AutenticacionController : ControllerBase
{
    private readonly IAutenticacionService _autenticacionService;
    private readonly ITokenService _tokenService;

    public AutenticacionController(IAutenticacionService autenticacionService, ITokenService tokenService)
    {
        _autenticacionService = autenticacionService;
        _tokenService = tokenService;
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

        string token = await _autenticacionService.AutenticarUsuario(dto.Email, dto.Contraseña);

        if (token == null) // si la autenticación falla, el token será null
        {
            return Unauthorized("Credenciales incorrectas.");
        }

        // si la autenticación es exitosa, se devuelve el token

        return Ok(new { Token = token });
    }
}

