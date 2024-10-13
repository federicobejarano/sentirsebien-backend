using sentirsebien_backend.Domain.Entities;

namespace sentirsebien_backend.DataAccess.Repositories
{
    public interface IUsuarioRepository
    {
        Usuario ObtenerPorId(Guid usuarioId);
        Usuario ObtenerPorId(int id);
        Usuario ObtenerPorNombreUsuario(string nombreUsuario);
        IEnumerable<Usuario> ObtenerTodos();
        void Agregar(Usuario usuario);
        void Actualizar(Usuario usuario);
        void Eliminar(int id);
    }

}
