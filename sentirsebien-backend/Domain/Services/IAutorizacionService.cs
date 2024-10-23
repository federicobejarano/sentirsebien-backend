using sentirsebien_backend.Domain.Entities;

namespace sentirsebien_backend.Domain.Services
{
    public interface IAutorizacionService
    {
        // verificar si usuario tiene permiso específico
        Task<bool> TienePermisoAsync(int usuarioId, string permiso);

        // verificar si usuario tiene acceso a acción específica
        Task<bool> TieneAccesoAsync(int usuarioId, string accion);

        // obtener permisos a partir de roles del usuario
        Task<IEnumerable<Permiso>> ObtenerPermisosPorRolesAsync(IEnumerable<Rol> roles);

        // obtener roles asociados a usuario
        Task<IEnumerable<Rol>> ObtenerRolesDeUsuarioAsync(int usuarioId);
    }
}
