using sentirsebien_backend.DataAccess.Repositories;
using sentirsebien_backend.Domain.ValueObjects;
using sentirsebien_backend.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sentirsebien_backend.Domain.Services
{
    public class LoginService : ILoginService
    {
        private readonly IAutenticacionService _autenticacionService;
        private readonly IAutorizacionService _autorizacionService;
        private readonly IUsuarioRepository _usuarioRepository;

        public LoginService(IAutenticacionService autenticacionService, IAutorizacionService autorizacionService, IUsuarioRepository usuarioRepository)
        {
            _autenticacionService = autenticacionService;
            _autorizacionService = autorizacionService;
            _usuarioRepository = usuarioRepository;
        }

        // Método principal: gestionar proceso completo de login
        public async Task<AutorizacionUsuario> LoginAsync(string email, string password)
        {
            // 1. autenticar usuario

            if (await UsuarioNoAutenticado(email, password))
            {
                throw new UnauthorizedAccessException("Credenciales inválidas.");
            }

            // 2. obtener usuario

            Usuario usuario = await _usuarioRepository.ObtenerPorEmailAsync(email);

            if (usuario == null)
            {
                throw new UnauthorizedAccessException("Usuario no encontrado."); // manejar excepción a nivel del controlador
            }

            // 3. devolver el objeto de valor AutorizacionUsuario

            return await _autorizacionService.ObtenerAutorizacionUsuarioAsync(usuario); // modificar: devolver token de autorización ??
        }

        public async Task<bool> UsuarioNoAutenticado(string email, string password)
        {
            return !(await AutenticarUsuarioAsync(email, password));
        }

        // Autenticar usuario
        private async Task<bool> AutenticarUsuarioAsync(string email, string password)
        {
            // Llama a la autenticación en el servicio
            return await _autenticacionService.AutenticarUsuario(email, password);
        }
    }
}
