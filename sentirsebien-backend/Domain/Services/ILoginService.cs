using sentirsebien_backend.Domain.ValueObjects;

namespace sentirsebien_backend.Domain.Services
{
    public interface ILoginService
    {
        // método principal : gestionar proceso completo de login (autenticación, roles/permisos y token).
        Task<AutorizacionUsuario> LoginAsync(string email, string password);
    }
}
