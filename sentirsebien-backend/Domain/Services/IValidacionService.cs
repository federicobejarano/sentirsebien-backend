using sentirsebien_backend.Domain.Exceptions;

namespace sentirsebien_backend.Domain.Services
{
    public interface IValidacionService
    {
        Task<bool> ValidarEmailExistente(string email);

        bool ValidarFormatoEmail(string email);

        bool ValidarFormatoContraseña(string contraseña);
    }
}

