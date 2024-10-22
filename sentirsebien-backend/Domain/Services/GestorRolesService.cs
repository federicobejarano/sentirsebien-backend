using sentirsebien_backend.DataAccess.Repositories;
using sentirsebien_backend.Domain.Entities;
using sentirsebien_backend.Domain.Shared;

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
        public async Task<Result> AsignarRol(Usuario usuario, string nombreRol)
        {
            var rol = await _rolRepository.GetByNombreAsync(nombreRol);
            if (rol == null)
            {
                return Result.Failure($"El rol '{nombreRol}' no existe.");
            }
            usuario.AsignarRol(rol);
            return Result.Success();
        }


        // asignar un rol a un usuario
        public async Task<Usuario> AsignarRolPorDefecto(Usuario usuario)
        {
            var rol = await _rolRepository.GetByNombreAsync("Cliente");
            if (rol == null)
            {
                throw new Exception("El rol por defecto 'Cliente' no existe.");
            }
            usuario.AsignarRol(rol);
            return usuario;
        }



        // eliminar un rol de un usuario

        // public void EliminarRol(int usuarioId, TipoRol tipoRol)

        // listar roles de un usuario

        /*
        public List<Rol> ObtenerRolesPorUsuario(int usuarioId)
        {
            var usuario = _usuarioRepository.ObtenerPorId(usuarioId);
            if (usuario == null)
            {
                throw new Exception("Usuario no encontrado.");
            }

            return _rolRepository.ObtenerRolesPorUsuario(usuarioId);
        }
        */
    }
}
