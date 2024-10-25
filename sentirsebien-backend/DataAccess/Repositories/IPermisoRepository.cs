using sentirsebien_backend.Domain.Entities;

namespace sentirsebien_backend.DataAccess.Repositories
{
    public interface IPermisoRepository
    {
        sentirsebien_backend.Domain.Entities.Permiso ObtenerPermisoPorNombre(string nombre);

        void CrearPermiso(sentirsebien_backend.Domain.Entities.Permiso permiso);

        void ActualizarPermiso(sentirsebien_backend.Domain.Entities.Permiso permiso);

        void EliminarPermiso(string nombre);

        Task <List<sentirsebien_backend.Domain.Entities.Permiso>> ObtenerPermisosPorRol(int rolId);

        Task<List<sentirsebien_backend.Domain.Entities.Permiso>> ObtenerPermisosPorUsuario(int usuarioId);

        Task<List<Permiso>> ObtenerPermisosPorRoles(IEnumerable<int> roleIds);
    }
}

