using sentirsebien_backend.Domain.Entities;

namespace sentirsebien_backend.DataAccess.Repositories
{
    public interface IUsuarioRepository
    {
        sentirsebien_backend.Domain.Entities.Usuario ObtenerPorId(int usuarioId);
        sentirsebien_backend.Domain.Entities.Usuario ObtenerPorNombreUsuario(string nombreUsuario);
        IEnumerable<Usuario> ObtenerTodos();
        void Agregar(Usuario usuario);
        void Actualizar(Usuario usuario);
        void Eliminar(int id);
    }
}
