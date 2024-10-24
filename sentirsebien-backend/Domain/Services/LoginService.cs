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
        private readonly ITokenService _tokenService;

        public LoginService(IAutenticacionService autenticacionService, IAutorizacionService autorizacionService, IUsuarioRepository usuarioRepository, ITokenService tokenService)
        {
            _autenticacionService = autenticacionService;
            _autorizacionService = autorizacionService;
            _usuarioRepository = usuarioRepository;
            _tokenService = tokenService;
        }

        // Método principal: gestionar proceso completo de login
        public async Task<TokenAutenticacion> LoginAsync(string email, string password)
        {
            Usuario usuario;
            DatosDeAutenticacionUsuario datosDeAutenticacion;
            DatosDeAutorizacionUsuario datosDeAutorizacion;

            // 1. autenticar usuario

            datosDeAutenticacion = await AutenticarUsuarioAsync(email, password);

            if (datosDeAutenticacion == null) { throw new UnauthorizedAccessException("Credenciales inválidas."); }

            // 2. obtener usuario

            usuario = await _usuarioRepository.ObtenerPorEmailAsync(email);

            if (usuario == null)
            {
                throw new UnauthorizedAccessException("Usuario no encontrado."); // manejar excepción a nivel del controlador
            }

            // 3. autorizar usuario

            datosDeAutorizacion = await _autorizacionService.ObtenerAutorizacionUsuarioAsync(usuario);

            // generar y devolver token

            return await _tokenService.GenerarTokenAsync(datosDeAutenticacion, datosDeAutorizacion);
        }

        // Autenticar usuario
        private async Task<DatosDeAutenticacionUsuario> AutenticarUsuarioAsync(string email, string password)
        {
            // Llama a la autenticación en el servicio
            return await _autenticacionService.AutenticarUsuarioAsync(email, password);
        }
    }
}
