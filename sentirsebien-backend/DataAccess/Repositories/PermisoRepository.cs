using sentirsebien_backend.DataAccess.DbContexts;
using sentirsebien_backend.Domain.Services;

namespace sentirsebien_backend.DataAccess.Repositories
{
    public class PermisoRepository : IPermisoRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IRolRepository _rolRepository;

        public PermisoRepository(ApplicationDbContext context, IRolRepository rolRepository)
        {
            _context = context;
            _rolRepository = rolRepository;
        }

        public sentirsebien_backend.Domain.Entities.Permiso ObtenerPermisoPorNombre(string nombre)
        {
            return _context.Permisos.FirstOrDefault(p => p.Nombre == nombre);
        }

        public void CrearPermiso(sentirsebien_backend.Domain.Entities.Permiso permiso)
        {
            _context.Permisos.Add(permiso);
            _context.SaveChanges();
        }

        public void ActualizarPermiso(sentirsebien_backend.Domain.Entities.Permiso permiso)
        {
            _context.Permisos.Update(permiso);
            _context.SaveChanges();
        }

        public void EliminarPermiso(string nombre)
        {
            var permiso = ObtenerPermisoPorNombre(nombre);
            if (permiso != null)
            {
                _context.Permisos.Remove(permiso);
                _context.SaveChanges();
            }
        }

        // obtener permisos por Rol, asegurando que el tipo coincida.
        public List<sentirsebien_backend.Domain.Entities.Permiso> ObtenerPermisosPorRol(int rolId)
        {
            var rol = _rolRepository.ObtenerRolPorId(rolId);
            if (rol == null)
            {
                throw new Exception("Rol no encontrado.");
            }

            if (SistemaPermisos.PermisosPorRol.TryGetValue(rol.Tipo, out List<sentirsebien_backend.Domain.Entities.Permiso> permisos))
            {
                return permisos;
            }

            throw new Exception("No se encontraron permisos para el rol especificado.");
        }

        // Este método selecciona correctamente los permisos del usuario, usando el namespace adecuado.
        public List<sentirsebien_backend.Domain.Entities.Permiso> ObtenerPermisosPorUsuario(int usuarioId)
        {
            // Primero obtenemos los roles del usuario
            var roles = _rolRepository.ObtenerRolesPorUsuario(usuarioId);

            // Luego obtenemos los permisos asociados a cada rol
            var permisos = new List<sentirsebien_backend.Domain.Entities.Permiso>();
            foreach (var rol in roles)
            {
                var permisosDelRol = _rolRepository.ObtenerPermisosPorRol(rol.Id);
                permisos.AddRange(permisosDelRol);
            }

            return permisos;
        }
    }
}
