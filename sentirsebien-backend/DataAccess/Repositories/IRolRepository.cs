using sentirsebien_backend.Domain.Entities;

namespace sentirsebien_backend.DataAccess.Repositories
{
    public interface IRolRepository
    {
        Rol ObtenerRolPorId(int id);
        Rol ObtenerRolPorNombre(string nombre);
        void CrearRol(sentirsebien_backend.Domain.Entities.Rol rol);
        void ActualizarRol(Rol rol);
        void EliminarRol(int id);
        List<Rol> ObtenerTodosLosRoles();
        List<Rol> ObtenerRolesPorUsuario(int usuarioId);

        // asignación y eliminación de roles de usuario
        void AsignarRolAUsuario(int usuarioId, Rol rol);
        void EliminarRolDeUsuario(int usuarioId, Rol rol);
    }
}
