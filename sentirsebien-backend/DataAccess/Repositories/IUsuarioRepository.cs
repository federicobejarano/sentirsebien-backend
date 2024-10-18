using sentirsebien_backend.Domain.Entities;
using sentirsebien_backend.Domain.Shared;

namespace sentirsebien_backend.DataAccess.Repositories
{
    public interface IUsuarioRepository
    {
        sentirsebien_backend.Domain.Entities.Usuario ObtenerPorId(int usuarioId);
        sentirsebien_backend.Domain.Entities.Usuario ValidarEmail(string email);
        sentirsebien_backend.Domain.Entities.Usuario ObtenerPorNombreUsuario(string nombreUsuario);
        IEnumerable<Usuario> ObtenerTodos();
        Result Agregar(sentirsebien_backend.Domain.Entities.Usuario usuario);
        void Actualizar(Usuario usuario);
        void Eliminar(int id);
    }
}
