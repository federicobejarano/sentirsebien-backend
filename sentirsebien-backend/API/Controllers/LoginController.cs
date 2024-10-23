using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using sentirsebien_backend.Application.DTOs;
using sentirsebien_backend.Domain.Services;

namespace sentirsebien_backend.API.Controllers
{
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
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        // endpoint para logueo de usuario
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequest)
        {
            // validar DTO
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                // llamar al servicio de logueo para gestionar el proceso
                string token = await _loginService.LoginAsync(loginRequest.Email, loginRequest.Password);

                // gestionar autenticación fallida
                if (string.IsNullOrEmpty(token))
                {
                    return Unauthorized(new { message = "Invalid credentials" });
                }

                // devolver el token generado
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                // manejo de errores generales
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
