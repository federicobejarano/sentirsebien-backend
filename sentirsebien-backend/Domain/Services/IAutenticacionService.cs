namespace sentirsebien_backend.Domain.Services
{
    public interface IAutenticacionService
    {
        Task<string> AutenticarUsuario(string username, string password); // autenticar con username y password
        Task<string> GenerarToken(string username); // generar un token JWT
        Task<bool> InvalidarToken(string token); // invalidar un token (logout)
    }
}
