using Microsoft.AspNetCore.Mvc;
using sentirsebien_backend.API.Dtos;
using sentirsebien_backend.DataAccess.Repositories;
using sentirsebien_backend.Domain.Services;
using sentirsebien_backend.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace sentirsebien_backend.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAutenticacionService _autenticacionService;
        private readonly IPasswordService _passwordService;
        private readonly TokenAutenticacion _tokenAutenticacion;
        private readonly ILogger<AutenticacionController> _logger; // inyectar logger

        public AutenticacionController(
            IUsuarioRepository usuarioRepository,
            IAutenticacionService autenticacionService,
            IPasswordService passwordService,
            TokenAutenticacion tokenAutenticacion,
            ILogger<AutenticacionController> logger)
        {
            _usuarioRepository = usuarioRepository;
            _autenticacionService = autenticacionService;
            _passwordService = passwordService;
            _tokenAutenticacion = tokenAutenticacion;
            _logger = logger; // inicializar logger
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                // Si el modelo no es válido, devolvemos los errores
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Intento de autenticación para usuario: {Usuario}", request.Username);

                // Proceso de autenticación
                var token = _autenticacionService.AutenticarUsuario(request.Username, request.Password);

                if (token == null)
                {
                    _logger.LogWarning("Autenticación fallida para usuario: {Usuario}", request.Username);
                    return Unauthorized(new { message = "Credenciales inválidas" });
                }

                _logger.LogInformation("Usuario autenticado: {Usuario}", request.Username);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en el proceso de autenticación para el usuario: {Usuario}", request.Username);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                // Si el modelo no es válido, devolvemos los errores
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Intento de registro para usuario: {Usuario}", request.Email);

                // Proceso de registro
                var result = _autenticacionService.RegistrarUsuario(request.Username, request.Email, request.Password);

                if (!result)
                {
                    _logger.LogWarning("Registro fallido para usuario: {Usuario}", request.Email);
                    return BadRequest(new { message = "Error al registrar el usuario" });
                }

                _logger.LogInformation("Usuario registrado exitosamente: {Usuario}", request.Email);
                return Ok(new { message = "Usuario registrado exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en el proceso de registro para el usuario: {Usuario}", request.Email);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // POST: api/auth/logout
        [HttpPost("logout")]
        public IActionResult Logout([FromBody] LogoutRequestDTO request)
        {
            // acá se puede implementar la invalidación del token si se almacena en algún lugar
            // por ahora solo retorna un mensaje genérico
            return Ok(new { mensaje = "Sesión cerrada correctamente" });
        }

        // GET: api/auth/verify
        [HttpGet("verify")]
        public IActionResult Verify([FromQuery] string token)
        {
            // verificar si el token es válido
            if (!_tokenAutenticacion.ValidarToken(token))
            {
                return Unauthorized(new { mensaje = "Token no válido o expirado" });
            }

            return Ok(new { mensaje = "Token válido" });
        }
    }
}
