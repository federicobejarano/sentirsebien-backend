using sentirsebien_backend.Domain.ValueObjects;

namespace sentirsebien_backend.Domain.Services
{
    public interface ILoginService
    {
        // método principal : gestionar proceso completo de login (autenticación, roles/permisos y token).
        Task<TokenAutenticacion> LoginAsync(string email, string password);
    }
}
