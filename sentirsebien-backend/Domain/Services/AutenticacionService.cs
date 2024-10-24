using sentirsebien_backend.DataAccess.Repositories;
using sentirsebien_backend.Domain.Entities;
using sentirsebien_backend.DataAccess.Repositories;
using sentirsebien_backend.Domain.Services;
using System.Threading.Tasks;
using sentirsebien_backend.Domain.ValueObjects;

namespace sentirsebien_backend.Application.Services
{
    public class AutenticacionService // : IAutenticacionService
    {
        private readonly IAutorizacionService _autorizacionService;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _tokenService;

        // inyectar las dependencias
        public AutenticacionService(IUsuarioRepository usuarioRepository, IPasswordService passwordService, ITokenService tokenService, IAutorizacionService autorizacionService)
        {
            _usuarioRepository = usuarioRepository;
            _passwordService = passwordService;
            _tokenService = tokenService;
            _autorizacionService = autorizacionService;
        }

        // autenticar (y autorizar) al usuario
        public async Task<TokenAutenticacion> AutenticarUsuarioAsync(string email, string password)
        {
            // obtener usuario o devolver null

            if ((await ObtenerUsuarioONull(email)) is not Usuario usuario) return null;

            // obtener datos de autenticación + autorización

            DatosDeAutenticacionUsuario datosDeAutenticacion = ObtenerDatosDeAutenticacion(usuario);
            DatosDeAutorizacionUsuario datosDeAutorizacion = await _autorizacionService.ObtenerAutorizacionUsuarioAsync(usuario);

            // obtener token

            return await ObtenerToken(datosDeAutenticacion, datosDeAutorizacion);
        }


        // invalidar token (implementación de logout)

        public async Task<bool> InvalidarToken(string token)
        {
            bool resultado = await _tokenService.InvalidarTokenAsync(token);

            if (!resultado) { return false; } // manejar invalidación fallida

            return true;
        }

        // métodos privados

        private async Task<Usuario> ObtenerUsuarioONull(string email)
        {
            Usuario usuario = await _usuarioRepository.ObtenerPorEmail(email);

            return usuario;
        }

        private DatosDeAutenticacionUsuario ObtenerDatosDeAutenticacion(Usuario usuario)
        {
            return new DatosDeAutenticacionUsuario(usuario.Id, usuario.Email);
        }

        // generar token JWT

        private async Task<TokenAutenticacion> ObtenerToken(DatosDeAutenticacionUsuario datosDeAutenticacion, DatosDeAutorizacionUsuario datosDeAutorizacion)
        {
            return await _tokenService.GenerarTokenAsync(datosDeAutenticacion, datosDeAutorizacion);
        }
    }
}
