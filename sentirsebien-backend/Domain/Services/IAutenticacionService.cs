namespace sentirsebien_backend.Domain.Services
{
    public interface IAutenticacionService
    {
        bool AutenticarUsuario(string username, string password);
        string GenerarToken(string username);
        void InvalidarToken(string token);
    }
}
