using sentirsebien_backend.Domain.Entities;

namespace sentirsebien_backend.DataAccess.Repositories
{
    public interface IPermisoRepository
    {
        Permiso ObtenerPermisoPorId(Guid id);
        void CrearPermiso(Permiso permiso);
        void ActualizarPermiso(Permiso permiso);
        void EliminarPermiso(Guid id);
        List<Permiso> ObtenerPermisosPorRol(Guid rolId);
        List<Permiso> ObtenerPermisosPorUsuario(Guid usuarioId); // <-- usado en GestorRolesService
    }


}
