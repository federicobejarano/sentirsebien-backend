namespace sentirsebien_backend.Domain.Services
{
    public interface ITokenService
    {
        Task<string> GenerarTokenAsync(string username); // generar token para un usuario autenticado
        Task<bool> ValidarTokenAsync(string token); // validar token existente
        Task<bool> InvalidarTokenAsync(string token); // invalidar token existente
    }
}
