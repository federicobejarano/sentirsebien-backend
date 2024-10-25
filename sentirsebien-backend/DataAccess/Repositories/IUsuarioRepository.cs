using sentirsebien_backend.Domain.Entities;
using sentirsebien_backend.Domain.Shared;

namespace sentirsebien_backend.DataAccess.Repositories
{
    public interface IUsuarioRepository
    {
        // consulta de usuarios

        Task<sentirsebien_backend.Domain.Entities.Usuario> ObtenerPorIdAsync(int usuarioId);

        Task<sentirsebien_backend.Domain.Entities.Usuario> ObtenerPorNombreUsuarioAsync(string nombreUsuario);

        Task<sentirsebien_backend.Domain.Entities.Usuario> ObtenerPorEmailAsync(string email);

        Task<IEnumerable<Usuario>> ObtenerTodosAsync();

        // modificación de usuarios

        Task<Result> AgregarAsync(sentirsebien_backend.Domain.Entities.Usuario usuario);

        Task ActualizarAsync(Usuario usuario);

        // otros

        Task<string> BuscarContraseñaAsync(int usuarioId);
    }
}
