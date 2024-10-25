using sentirsebien_backend.Domain.ValueObjects;
using sentirsebien_backend.Domain.Exceptions;

namespace sentirsebien_backend.Domain.Services
{
    public class LoginService : ILoginService
    {
        private readonly IAutenticacionService _autenticacionService;
        private readonly IValidacionService _validacionService;

        public LoginService(IAutenticacionService autenticacionService, IValidacionService validacionService)
        {
            _autenticacionService = autenticacionService;
            _validacionService = validacionService;
        }

        public async Task<TokenAutenticacion> LoginAsync(string email, string password)
        {
            // validar formato de email antes de continuar

            if (!_validacionService.ValidarFormatoEmail(email))
            {
                throw new FormatoInvalidoException("El formato del email es inválido.");
            }

            // delegar tarea de autenticación

            return await _autenticacionService.AutenticarUsuarioAsync(email, password);
        }
    }
}
