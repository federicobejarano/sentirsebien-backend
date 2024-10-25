using sentirsebien_backend.DataAccess.Repositories;
using sentirsebien_backend.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace sentirsebien_backend.Domain.Services
{
    public class ValidacionService : IValidacionService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPasswordService _passwordService;

        public ValidacionService(IUsuarioRepository usuarioRepository, IPasswordService passwordService)
        {
            _usuarioRepository = usuarioRepository;
            _passwordService = passwordService;
        }

        public async Task<bool> ValidarEmailExistente(string email)
        {
            var usuarioExistente = await _usuarioRepository.ObtenerPorEmailAsync(email);
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
