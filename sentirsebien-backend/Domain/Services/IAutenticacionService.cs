namespace sentirsebien_backend.Domain.Services
{
    public interface IAutenticacionService
    {
        bool AutenticarUsuario(string username, string password); // autenticar con username y password
        string GenerarToken(string username);                     // generar un token JWT
        void InvalidarToken(string token);                        // invalidar un token (logout)
        bool RegistrarUsuario(string username, string email, string password);
    }

}
