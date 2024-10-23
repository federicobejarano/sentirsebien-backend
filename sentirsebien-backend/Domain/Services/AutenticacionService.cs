using sentirsebien_backend.DataAccess.Repositories;
using sentirsebien_backend.Domain.Entities;
using sentirsebien_backend.DataAccess.Repositories;
using sentirsebien_backend.Domain.Services;
using System.Threading.Tasks;
using sentirsebien_backend.DataAccess.Models;

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
        public async Task<bool> AutenticarUsuario(string email, string password)
        {
            // validar el email del usuario (obtener la entidad Usuario)
            sentirsebien_backend.Domain.Entities.Usuario usuario = await _usuarioRepository.ValidarEmail(email);

            if (usuario == null) { return false; }

            // retornar resultado de validación de password
            return await PasswordValida(usuario, password);
        }

        private async Task<bool> PasswordValida(sentirsebien_backend.Domain.Entities.Usuario usuario, string password)
        {
            return _passwordService.VerifyPassword(usuario.Contraseña, password);
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
