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

        // asignar un rol a un usuario
        public void AsignarRol(int usuarioId, TipoRol tipoRol)
        {
            var usuario = _usuarioRepository.ObtenerPorId(usuarioId);
            if (usuario == null)
            {
                throw new Exception("Usuario no encontrado.");
            }

            // convertir TipoRol (enum) a Rol (entidad)
            var rol = ConvertirTipoRolAEntidadRol(tipoRol);

            _rolRepository.AsignarRolAUsuario(usuarioId, rol);
        }

        // eliminar un rol de un usuario
        public void EliminarRol(int usuarioId, TipoRol tipoRol)
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

        // listar roles de un usuario
        public List<Rol> ObtenerRolesPorUsuario(int usuarioId)
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
