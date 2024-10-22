namespace sentirsebien_backend.Domain.Services
{
    public interface IAutorizacionService
    {
        // método 1. validar fecha de expiración del token

        // método 2. validar firma del token

        // método 3. validar token (llama a métodos 1 y 2)

        // método 4. delegar gestión de roles a GestorRolesService

        // método 5. asignar permisos según los roles

        // método 6. punto de entrada al service de autorización
        //  -   gestiona recursos y funcionalidades disponibles (en otros services) llamando a los métodos 1 a 5

        // verificar si un usuario tiene un permiso específico
        bool TienePermiso(int usuarioId, string permiso);

        // verificar si un usuario tiene acceso a una acción específica
        bool TieneAcceso(int usuarioId, string accion);
    }
}
