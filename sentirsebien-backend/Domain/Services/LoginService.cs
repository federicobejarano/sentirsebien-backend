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
                throw new UnauthorizedAccessException("Usuario no encontrado."); // manenar excepción a nivel del controlador
            }

            // 3. obtener roles

            var roles = await ObtenerRolesUsuarioAsync(usuario.Id);

            // 4. obtener permisos

            var permisos = await ObtenerPermisosPorRolesAsync(roles);

            // 5. crear y devolver el objeto de valor AutorizacionUsuario
            return new AutorizacionUsuario(usuario.Id, email, roles.ToList(), permisos.ToList());
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

        // Obtener los roles del usuario
        private async Task<IEnumerable<Rol>> ObtenerRolesUsuarioAsync(int userId)
        {
            // Llama al método del repositorio de roles para obtener los roles por el ID del usuario
            return await _autorizacionService.ObtenerRolesDeUsuarioAsync(userId);
        }

        // Obtener permisos basados en los roles del usuario
        private async Task<IEnumerable<Permiso>> ObtenerPermisosPorRolesAsync(IEnumerable<Rol> roles)
        {
            var permisos = new HashSet<Permiso>();

            // Itera sobre los roles y obtiene los permisos correspondientes para cada uno
            foreach (var rol in roles)
            {
                var permisosPorRol = await _autorizacionService.ObtenerPermisosPorRolesAsync(new List<Rol> { rol });
                foreach (var permiso in permisosPorRol)
                {
                    permisos.Add(permiso); // El HashSet evita duplicados automáticamente
                }
            }

            return permisos;
        }
    }
}
