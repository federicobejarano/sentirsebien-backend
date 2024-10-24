using sentirsebien_backend.Domain.ValueObjects;

namespace sentirsebien_backend.Domain.Services
{
    public interface IAutenticacionService
    {
        // Task<bool> AutenticarUsuarioAsync(string email, string password); // autenticar con email y password
        Task<DatosDeAutenticacionUsuario> AutenticarUsuarioAsync(string email, string password);

        // Task<string> GenerarToken(string username); // generar un token JWT
        Task<bool> InvalidarToken(string token); // invalidar un token (logout)
    }
}
