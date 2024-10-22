using sentirsebien_backend.API.Dtos;
using sentirsebien_backend.Domain.Shared;

namespace sentirsebien_backend.Domain.Services
{
    public interface IRegistroUsuarioService
    {
        sentirsebien_backend.Domain.Entities.Usuario CrearUsuario(RegistroUsuarioDTO dto);

        Task<Result> RegistrarUsuario(RegistroUsuarioDTO dto);
    }
}
