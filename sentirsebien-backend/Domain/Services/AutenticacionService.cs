using sentirsebien_backend.DataAccess.Repositories;
using sentirsebien_backend.Domain.Entities;
using sentirsebien_backend.Domain.Exceptions;
using sentirsebien_backend.Domain.Services;
using sentirsebien_backend.Domain.ValueObjects;

namespace sentirsebien_backend.Application.Services
{
    public class AutenticacionService : IAutenticacionService
    {
        private readonly IAutorizacionService _autorizacionService;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _tokenService;

        // constructor para inyección de dependencias
        public AutenticacionService(
            IUsuarioRepository usuarioRepository,
            IPasswordService passwordService,
            ITokenService tokenService,
            IAutorizacionService autorizacionService)
        {
            _usuarioRepository = usuarioRepository;
            _passwordService = passwordService;
            _tokenService = tokenService;
            _autorizacionService = autorizacionService;
        }

        // autenticar y autorizar al usuario
        public async Task<TokenAutenticacion> AutenticarUsuarioAsync(string email, string contraseña)
        {
            // obtener el usuario, o lanzar excepción si no existe
            var usuario = await ObtenerUsuarioPorEmail(email)
                ?? throw new UsuarioNoEncontradoException("El usuario no existe o el email es incorrecto.");

            // validar contraseña
            if (!await EsContraseñaValida(contraseña, usuario.Id))
                throw new ContraseñaInvalidaException("La contraseña proporcionada es incorrecta.");

            // generar los datos de autenticación y autorización
            var datosDeAutenticacion = new DatosDeAutenticacionUsuario(usuario.Id, usuario.Email);
            var datosDeAutorizacion = await _autorizacionService.ObtenerAutorizacionUsuarioAsync(usuario);

            // generar y retornar el token JWT
            return await _tokenService.GenerarTokenAsync(datosDeAutenticacion, datosDeAutorizacion);
        }

        // invalidar el token (para logout)
        public async Task<bool> InvalidarToken(string token)
        {
            return await _tokenService.InvalidarTokenAsync(token);
        }

        // métodos privados

        private async Task<Usuario> ObtenerUsuarioPorEmail(string email)
        {
            return await _usuarioRepository.ObtenerPorEmailAsync(email);
        }

        private async Task<bool> EsContraseñaValida(string contraseñaIngresada, int idUsuario)
        {
            var hashAlmacenado = await _usuarioRepository.BuscarContraseñaAsync(idUsuario);

            if (string.IsNullOrEmpty(hashAlmacenado))
                throw new ContraseñaInvalidaException("No se encontró una contraseña almacenada para el usuario.");

            return _passwordService.VerifyPassword(hashAlmacenado, contraseñaIngresada);
        }
    }
}

