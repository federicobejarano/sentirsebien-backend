using sentirsebien_backend.Domain.Entities;

namespace sentirsebien_backend.Domain.Services
{
    public interface ILoginService
    {
        // método principal : gestionar proceso completo de login (autenticación, roles/permisos y token).
        Task<string> LoginAsync(string email, string password);
        // autenticar usuario. Valida email y contraseña llamando a IAutenticacionService.
        Task<bool> AutenticarUsuarioAsync(string email, string password);

        // obtener los roles del usuario a través del IRolRepository.
        Task<IEnumerable<string>> ObtenerRolesUsuarioAsync(int userId);

        // obtener permisos basados en roles del usuario, llamando a IPermisoRepository.
        Task<IEnumerable<string>> ObtenerPermisosPorRolesAsync(IEnumerable<string> roles);

        // generar el token de autenticación basado en la entidad de dominio LoginUsuario
        Task<string> GenerarTokenAsync(LoginUsuario loginUsuario);
    }
}
