using sentirsebien_backend.Domain.Entities;
using sentirsebien_backend.Domain.Shared;

namespace sentirsebien_backend.DataAccess.Repositories
{
    public interface IUsuarioRepository
    {
        /*
        sentirsebien_backend.Domain.Entities.Usuario ObtenerPorId(int usuarioId);

        // sentirsebien_backend.Domain.Entities.Usuario ValidarEmail(string email); <- modificado
        Task<sentirsebien_backend.Domain.Entities.Usuario> ValidarEmail(string email);
        sentirsebien_backend.Domain.Entities.Usuario ObtenerPorNombreUsuario(string nombreUsuario);
        IEnumerable<Usuario> ObtenerTodos();
        Result Agregar(sentirsebien_backend.Domain.Entities.Usuario usuario);
        void Actualizar(Usuario usuario);
        void Eliminar(int id);

        */

        Task<sentirsebien_backend.Domain.Entities.Usuario> ObtenerPorIdAsync(int usuarioId);

        Task<sentirsebien_backend.Domain.Entities.Usuario> ObtenerPorEmail(string email);

        Task<sentirsebien_backend.Domain.Entities.Usuario> ObtenerPorNombreUsuarioAsync(string nombreUsuario);

        Task<sentirsebien_backend.Domain.Entities.Usuario> ObtenerPorEmailAsync(string email);

        Task<IEnumerable<Usuario>> ObtenerTodosAsync();

        Task<Result> AgregarAsync(sentirsebien_backend.Domain.Entities.Usuario usuario);

        Task ActualizarAsync(Usuario usuario);

        Task EliminarAsync(int id);
    }
}
