using sentirsebien_backend.API.Dtos;
using sentirsebien_backend.Domain.Shared;

namespace sentirsebien_backend.Domain.Services
{
    public interface IRegistroUsuarioService
    {
        // 1. validar si un usuario ya existe en la base de datos por su email
        bool ValidarUsuarioExistente(string email);

        // 2. crear una instancia de la entidad de dominio Usuario a partir del DTO
        sentirsebien_backend.Domain.Entities.Usuario CrearUsuario(RegistroUsuarioDTO dto);

        // 3. manejar todo el proceso de registro de usuario (llamando a métodos anteriores)
        Task<Result> RegistrarUsuario(RegistroUsuarioDTO dto);
    }
}
