using sentirsebien_backend.Domain.Entities;

namespace sentirsebien_backend.DataAccess.Repositories
{
    public interface IRolRepository
    {
        Rol ObtenerRolPorId(Guid id);
        Rol ObtenerRolPorNombre(string nombre);
        void CrearRol(Rol rol);
        void ActualizarRol(Rol rol);
        void EliminarRol(Guid id);
        List<Rol> ObtenerTodosLosRoles();
        List<Rol> ObtenerRolesPorUsuario(Guid usuarioId);

        // asignación y eliminación de roles de usuario
        void AsignarRolAUsuario(Guid usuarioId, Rol rol);
        void EliminarRolDeUsuario(Guid usuarioId, Rol rol);
    }
}
