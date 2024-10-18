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
        public async Task AsignarRol(sentirsebien_backend.Domain.Entities.Usuario usuario, string nombreRol)
        {
            // Obtener el rol por su nombre de forma asíncrona
            var rol = await _rolRepository.GetByNombreAsync(nombreRol);

            if (rol == null)
            {
                throw new Exception($"El rol '{nombreRol}' no existe.");
            }

            // Asignar el rol al usuario
            usuario.AsignarRol(rol); // Asumiendo que tienes un método AsignarRol en la entidad Usuario

            // Aquí podrías hacer otras acciones, como guardar los cambios en el repositorio si es necesario.
        }


        // eliminar un rol de un usuario

        // public void EliminarRol(int usuarioId, TipoRol tipoRol)

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
    }
}
