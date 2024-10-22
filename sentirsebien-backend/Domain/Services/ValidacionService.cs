using sentirsebien_backend.DataAccess.Repositories;
using System.Text.RegularExpressions;

namespace sentirsebien_backend.Domain.Services
{
    public class ValidacionService : IValidacionService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public ValidacionService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<bool> ValidarEmailExistente(string email)
        {
            var usuarioExistente = await _usuarioRepository.ValidarEmail(email);
            return usuarioExistente != null;
        }

        public bool ValidarFormatoEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        public bool ValidarFormatoContraseña(string contraseña)
        {
            return contraseña.Length >= 6;
        }
    }
}
