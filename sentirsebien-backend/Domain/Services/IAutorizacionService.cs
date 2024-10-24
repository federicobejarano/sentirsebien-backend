using sentirsebien_backend.Domain.Entities;
using sentirsebien_backend.Domain.ValueObjects;

namespace sentirsebien_backend.Domain.Services
{
    public interface IAutorizacionService
    {

        // obtener permisos asociados a los roles del usuario
        Task<DatosDeAutorizacionUsuario> ObtenerAutorizacionUsuarioAsync(Usuario usuario);

        /**** implementar después ****/

        // verificar si un usuario tiene un permiso específico
        // Task<bool> TienePermisoAsync(int usuarioId, string permiso);

        // verificar si un usuario tiene acceso a una acción específica
        // Task<bool> TieneAccesoAsync(int usuarioId, string accion);
    }
}
