using sentirsebien_backend.Domain.Entities;
using sentirsebien_backend.Domain.ValueObjects;

namespace sentirsebien_backend.Domain.Services
{
    public interface IAutorizacionService
    {
        // verificar si un usuario tiene un permiso específico
        Task<bool> TienePermisoAsync(int usuarioId, string permiso);

        // verificar si un usuario tiene acceso a una acción específica
        Task<bool> TieneAccesoAsync(int usuarioId, string accion);

        // obtener permisos asociados a los roles del usuario
        Task<IEnumerable<Permiso>> ObtenerPermisosPorRolesAsync(IEnumerable<Rol> roles);

        // obtener roles asociados a un usuario
        Task<IEnumerable<Rol>> ObtenerRolesDeUsuarioAsync(int usuarioId);

        // obtener tanto roles como permisos del usuario en un objeto de valor (AutorizacionUsuario)
        Task<AutorizacionUsuario> ObtenerAutorizacionUsuarioAsync(int usuarioId, string email, string password);
    }
}
