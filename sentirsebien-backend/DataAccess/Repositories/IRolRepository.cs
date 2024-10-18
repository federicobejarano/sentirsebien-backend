using sentirsebien_backend.Domain.Entities;

namespace sentirsebien_backend.DataAccess.Repositories
{
    public interface IRolRepository
    {
        // las funcionalidades de este repository solo son accedidas por usuarios con rol 'Administrador'

        // Rol ObtenerRolPorId(int id);
        // Rol ObtenerRolPorNombre(string nombre);

        public Task<Rol> GetByNombreAsync(string nombreRol);

        void ActualizarRol(Rol rol);
        void CrearRol(sentirsebien_backend.Domain.Entities.Rol rol);
        void EliminarRol(int id);
        List<Rol> ObtenerTodosLosRoles();
        List<Rol> ObtenerRolesPorUsuario(int usuarioId);

        // asignación y eliminación de roles de usuario

        // void AsignarRolAUsuario(int usuarioId, Rol rol); <-- debe hacerse a nivel de dominio
        // void EliminarRolDeUsuario(int usuarioId, Rol rol); <-- debe hacerse a nivel de dominio

        // List<Permiso> ObtenerPermisosPorRol(int rolId); <-- revisar
    }
}
