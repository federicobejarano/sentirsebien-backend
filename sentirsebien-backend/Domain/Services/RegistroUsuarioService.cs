using sentirsebien_backend.DataAccess.Repositories;

namespace sentirsebien_backend.Domain.Services
{
    public class RegistroUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAutenticacionService _autenticacionService;
        private readonly IAutorizacionService _autorizacionService;
        private readonly IGestorRolesService _gestorRolesService;
        private readonly PasswordService _passwordService;

        // constructor con inyección de dependencias
        public RegistroUsuarioService(
            IUsuarioRepository usuarioRepository,
            IAutenticacionService autenticacionService,
            IAutorizacionService autorizacionService,
            IGestorRolesService gestorRolesService,
            PasswordService passwordService)
        {
            _usuarioRepository = usuarioRepository;
            _autenticacionService = autenticacionService;
            _autorizacionService = autorizacionService;
            _gestorRolesService = gestorRolesService;
            _passwordService = passwordService;
        }

        // método para registrar un nuevo usuario
        public void RegistrarUsuario(RegistroUsuarioDTO dto)
        {
            // 1. validar que el email no esté registrado
            var usuarioExistente = _usuarioRepository.ObtenerPorEmail(dto.Email);
            if (usuarioExistente != null)
            {
                throw new Exception("El email ya está registrado.");
            }

            // 2. validar que el nombre de usuario no esté registrado
            var nombreUsuarioExistente = _usuarioRepository.ObtenerPorNombreUsuario(dto.NombreUsuario);
            if (nombreUsuarioExistente != null)
            {
                throw new Exception("El nombre de usuario ya está en uso.");
            }

            // 3. crear una nueva instancia del usuario
            var nuevoUsuario = new Usuario(
                dto.NombreUsuario,
                dto.Email,
                _passwordService.HashPassword(dto.Contrasena)
            );

            // 4. asignar el rol de "Cliente" por defecto
            var rolCliente = _gestorRolesService.ObtenerRolPorTipo(TipoRol.Cliente);
            _gestorRolesService.AsignarRol(nuevoUsuario, rolCliente);

            // 5. guardar el nuevo usuario en el repositorio
            _usuarioRepository.Agregar(nuevoUsuario);
        }
    }

}
