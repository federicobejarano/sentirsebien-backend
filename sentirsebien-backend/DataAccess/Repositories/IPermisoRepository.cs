using sentirsebien_backend.Domain.Entities;

namespace sentirsebien_backend.DataAccess.Repositories
{
    public interface IPermisoRepository
    {
        sentirsebien_backend.Domain.Entities.Permiso ObtenerPermisoPorId(int id);
        void CrearPermiso(sentirsebien_backend.Domain.Entities.Permiso permiso);
        void ActualizarPermiso(sentirsebien_backend.Domain.Entities.Permiso permiso);
        void EliminarPermiso(int id);
        List<sentirsebien_backend.Domain.Entities.Permiso> ObtenerPermisosPorRol(int rolId);
        List<sentirsebien_backend.Domain.Entities.Permiso> ObtenerPermisosPorUsuario(int usuarioId);
    }
}

