using sentirsebien_backend.Domain.Entities;

namespace sentirsebien_backend.Domain.Services
{
    public interface IGestorRolesService
    {
        // asignar un rol a un usuario
        void AsignarRol(int usuarioId, TipoRol tipoRol);

        // eliminar un rol de un usuario
        void EliminarRol(int usuarioId, TipoRol tipoRol);

        // obtener la lista de roles asignados a un usuario
        List<Rol> ObtenerRolesPorUsuario(int usuarioId);
    }
}
