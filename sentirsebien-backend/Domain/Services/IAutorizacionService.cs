namespace sentirsebien_backend.Domain.Services
{
    public interface IAutorizacionService
    {
        // verificar si un usuario tiene un rol específico
        bool TieneRol(Guid usuarioId, TipoRol rol);

        // verificar si un usuario tiene un permiso específico
        bool TienePermiso(Guid usuarioId, string permiso);

        // verificar si un usuario tiene acceso a una acción específica
        bool TieneAcceso(Guid usuarioId, string accion);
    }
}
