
using global::sentirsebien_backend.DataAccess.DbContexts;
using Microsoft.EntityFrameworkCore;
using sentirsebien_backend.Domain.Entities;

namespace sentirsebien_backend.DataAccess.Repositories
{

    namespace sentirsebien_backend.DataAccess.Repositories
    {
        public class UsuarioRepository : IUsuarioRepository
        {
            private readonly ApplicationDbContext _context;

            public UsuarioRepository(ApplicationDbContext context)
            {
                _context = context;
            }

            public Usuario ObtenerPorId(Guid usuarioId)
            {
                return _context.Usuarios.Find(usuarioId);
            }

            public Usuario ObtenerPorId(int id)
            {
                return _context.Usuarios.Find(id);
            }

            public Usuario ObtenerPorNombreUsuario(string nombreUsuario)
            {
                return _context.Usuarios.FirstOrDefault(u => u.NombreUsuario == nombreUsuario);
            }

            public IEnumerable<Usuario> ObtenerTodos()
            {
                return _context.Usuarios.ToList();
            }

            public void Agregar(Usuario usuario)
            {
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();
            }

            public void Actualizar(Usuario usuario)
            {
                _context.Usuarios.Update(usuario);
                _context.SaveChanges();
            }

            public void Eliminar(int id)
            {
                var usuario = _context.Usuarios.Find(id);
                if (usuario != null)
                {
                    _context.Usuarios.Remove(usuario);
                    _context.SaveChanges();
                }
            }
        }
    }
}
