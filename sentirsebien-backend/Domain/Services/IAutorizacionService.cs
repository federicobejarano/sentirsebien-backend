namespace sentirsebien_backend.Domain.Services
{
    public interface IAutorizacionService
    {
        // verificar si un usuario tiene un rol específico
        bool TieneRol(int usuarioId, TipoRol rol);

        // verificar si un usuario tiene un permiso específico
        bool TienePermiso(int usuarioId, string permiso);

        // verificar si un usuario tiene acceso a una acción específica
        bool TieneAcceso(int usuarioId, string accion);
    }
}
