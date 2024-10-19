using sentirsebien_backend.DataAccess.Repositories;
using sentirsebien_backend.Domain.Entities;
using sentirsebien_backend.DataAccess.Repositories;
using sentirsebien_backend.Domain.Services;
using System.Threading.Tasks;

namespace sentirsebien_backend.Application.Services
{
    public class AutenticacionService : IAutenticacionService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _tokenService;

        // inyectar las dependencias
        public AutenticacionService(IUsuarioRepository usuarioRepository, IPasswordService passwordService, ITokenService tokenService)
        {
            _usuarioRepository = usuarioRepository;
            _passwordService = passwordService;
            _tokenService = tokenService;
        }

        // autenticar al usuario
        public async Task<string> AutenticarUsuario(string email, string password)
        {
            // 1. validar el email del usuario (obtener la entidad Usuario)
            Usuario usuario = await _usuarioRepository.ValidarEmail(email); // error: "Usuario" no posee una definición para `GetAwaiter`

            if (usuario == null)
            {
                // si el usuario no existe, retornar null
                return null;
            }

            // 2. verificar la contraseña (comparar el hash almacenado con la ingresada)
            bool passwordValida = _passwordService.VerifyPassword(usuario.Contraseña, password);

            if (!passwordValida)
            {
                // si la contraseña no es válida, retornar null
                return null;
            }

            // 3. generar el token JWT usando el servicio de tokens
            string token = await _tokenService.GenerarTokenAsync(usuario.Email);

            return token; // Si la autenticación es exitosa, retornar el token generado
        }

        // generar un token (puede ser utilizado si se requiere en otro contexto)
        public async Task<string> GenerarToken(string username)
        {
            // Llamar al servicio de tokens para generar un nuevo token JWT
            return await _tokenService.GenerarTokenAsync(username);
        }

        // invalidar un token (implementación de logout)
        public async Task<bool> InvalidarToken(string token)
        {
            // invalidar token a través del servicio de tokens
            bool resultado = await _tokenService.InvalidarTokenAsync(token);

            if (!resultado)
            {
                // manejar invalidación fallida
                return false;
            }

            // invalidación exitosa
            return true;
        }

    }
}
