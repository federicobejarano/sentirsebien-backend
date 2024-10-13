using sentirsebien_backend.DataAccess.Repositories;
using sentirsebien_backend.Domain.Entities;

namespace sentirsebien_backend.Domain.Services
{
    public class GestorRolesService : IGestorRolesService
    {
        private readonly IRolRepository _rolRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public GestorRolesService(IRolRepository rolRepository, IUsuarioRepository usuarioRepository)
        {
            _rolRepository = rolRepository;
            _usuarioRepository = usuarioRepository;
        }

        // Asigna un rol a un usuario
        public void AsignarRol(Guid usuarioId, TipoRol tipoRol)
        {
            var usuario = _usuarioRepository.ObtenerPorId(usuarioId);
            if (usuario == null)
            {
                throw new Exception("Usuario no encontrado.");
            }

            // Convertir TipoRol (enum) a Rol (entidad)
            var rol = ConvertirTipoRolAEntidadRol(tipoRol);

            _rolRepository.AsignarRolAUsuario(usuarioId, rol);
        }

        // Elimina un rol de un usuario
        public void EliminarRol(Guid usuarioId, TipoRol tipoRol)
        {
            var usuario = _usuarioRepository.ObtenerPorId(usuarioId);
            if (usuario == null)
            {
                throw new Exception("Usuario no encontrado.");
            }

            // Convertir TipoRol (enum) a Rol (entidad)
            var rol = ConvertirTipoRolAEntidadRol(tipoRol);

            _rolRepository.EliminarRolDeUsuario(usuarioId, rol);
        }

        // Lista los roles de un usuario
        public List<Rol> ObtenerRolesPorUsuario(Guid usuarioId)
        {
            var usuario = _usuarioRepository.ObtenerPorId(usuarioId);
            if (usuario == null)
            {
                throw new Exception("Usuario no encontrado.");
            }

            return _rolRepository.ObtenerRolesPorUsuario(usuarioId);
        }

        // convertir un TipoRol (enum) a Rol (entidad)
        private Rol ConvertirTipoRolAEntidadRol(TipoRol tipoRol)
        {
            return _rolRepository.ObtenerRolPorNombre(tipoRol.ToString());
        }
    }

}
