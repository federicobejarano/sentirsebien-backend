using sentirsebien_backend.Domain.ValueObjects;

namespace sentirsebien_backend.Domain.Services
{
    public class LoginService : ILoginService
    {
        private readonly IAutenticacionService _autenticacionService;

        public LoginService(IAutenticacionService autenticacionService)
        {
            _autenticacionService = autenticacionService;
        }

        // método principal : delegar autenticación + obtener token
        public async Task<TokenAutenticacion> LoginAsync(string email, string password)
        {

            return await _autenticacionService.AutenticarUsuarioAsync(email, password);
        }
    }
}
