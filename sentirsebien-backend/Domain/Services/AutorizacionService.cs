using sentirsebien_backend.DataAccess.Repositories;

namespace sentirsebien_backend.Domain.Services
{
    public class AutorizacionService : IAutorizacionService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IRolRepository _rolRepository;
        private readonly IPermisoRepository _permisoRepository;

        public AutorizacionService(IUsuarioRepository usuarioRepository, IRolRepository rolRepository, IPermisoRepository permisoRepository)
        {
            _usuarioRepository = usuarioRepository;
            _rolRepository = rolRepository;
            _permisoRepository = permisoRepository;
        }

        // verificar si el usuario tiene un rol específico
        public bool TieneRol(int usuarioId, TipoRol rol)
        {
            var usuario = _usuarioRepository.ObtenerPorId(usuarioId);
            if (usuario == null) return false;

            var roles = _rolRepository.ObtenerRolesPorUsuario(usuarioId);
            return roles.Any(r => r.Tipo == rol);
        }

        // verificar si el usuario tiene un permiso específico
        public bool TienePermiso(int usuarioId, string permiso)
        {
            var usuario = _usuarioRepository.ObtenerPorId(usuarioId);
            if (usuario == null) return false;

            var permisos = _permisoRepository.ObtenerPermisosPorUsuario(usuarioId);
            return permisos.Any(p => p.Nombre == permiso);
        }

        // verificar si el usuario tiene acceso a una acción específica
        public bool TieneAcceso(int usuarioId, string accion)
        {
            // en este caso, la acción puede mapearse a un permiso específico
            return TienePermiso(usuarioId, accion);
        }
    }

}
