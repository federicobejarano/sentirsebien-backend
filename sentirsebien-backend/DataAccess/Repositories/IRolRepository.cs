using sentirsebien_backend.Domain.Entities;

namespace sentirsebien_backend.DataAccess.Repositories
{
    public interface IRolRepository
    {
        // Rol ObtenerRolPorId(int id);

        // Rol ObtenerRolPorNombre(string nombre);

        public Task<Rol> GetByNombreAsync(string nombreRol);

        void ActualizarRol(Rol rol);
        void CrearRol(sentirsebien_backend.Domain.Entities.Rol rol);
        void EliminarRol(int id);

        public Task<List<Rol>> ObtenerTodosLosRoles();
        public Task<List<Rol>> ObtenerRolesPorUsuario(int usuarioId);

        // void AsignarRolAUsuario(int usuarioId, Rol rol); <-- debe hacerse a nivel de dominio

        // void EliminarRolDeUsuario(int usuarioId, Rol rol); <-- debe hacerse a nivel de dominio

        // List<Permiso> ObtenerPermisosPorRol(int rolId); <-- revisar
    }
}
