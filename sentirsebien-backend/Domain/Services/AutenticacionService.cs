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
            Usuario usuario = await _usuarioRepository.ValidarEmail(email);

            if (usuario == null) { return null; }

            // 2. verificar la contraseña (comparar el hash almacenado con la ingresada)
            bool passwordValida = _passwordService.VerifyPassword(usuario.Contraseña, password);

            if (!passwordValida) { return null; }

            // 3. generar el token JWT usando el servicio de tokens
            return await _tokenService.GenerarTokenAsync(usuario.Email);
        }

        // generar token JWT

        public async Task<string> GenerarToken(string username)
        {
            return await _tokenService.GenerarTokenAsync(username);
        }

        // invalidar token (implementación de logout)

        public async Task<bool> InvalidarToken(string token)
        {
            bool resultado = await _tokenService.InvalidarTokenAsync(token);

            if (!resultado) { return false; } // manejar invalidación fallida

            return true;
        }

    }
}
