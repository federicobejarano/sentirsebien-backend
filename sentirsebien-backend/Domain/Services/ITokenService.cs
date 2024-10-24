using sentirsebien_backend.Domain.ValueObjects;

namespace sentirsebien_backend.Domain.Services
{
    public interface ITokenService
    {
        // Task<string> GenerarTokenAsync(string username); // generar token para un usuario autenticado

        Task<TokenAutenticacion> GenerarTokenAsync(DatosDeAutenticacionUsuario datosDeAutenticacion, DatosDeAutorizacionUsuario datosDeAutorizacion);

        Task<bool> ValidarTokenAsync(string token); // validar token existente
        Task<bool> InvalidarTokenAsync(string token); // invalidar token existente

        // método 1. validar fecha de expiración del token

        // método 2. validar firma del token

        // método 3. validar token (llama a métodos 1 y 2)

        // método 4. delegar gestión de roles a GestorRolesService

        // método 5. asignar permisos según los roles

        // método 6. punto de entrada al service de autorización
    }
}
