using AutoMapper;
using Microsoft.EntityFrameworkCore;
using sentirsebien_backend.DataAccess.DbContexts;
using sentirsebien_backend.Domain.Shared;

namespace sentirsebien_backend.DataAccess.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UsuarioRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // métodos de consulta de usuarios

        public async Task<sentirsebien_backend.Domain.Entities.Usuario> ObtenerPorIdAsync(int usuarioId)
        {
            var usuarioDb = await _context.Usuarios.FirstOrDefaultAsync(u => u.ID == usuarioId);
            return usuarioDb == null ? null : _mapper.Map<sentirsebien_backend.Domain.Entities.Usuario>(usuarioDb);
        }

        public async Task<sentirsebien_backend.Domain.Entities.Usuario> ObtenerPorNombreUsuarioAsync(string nombreUsuario)
        {
            var usuarioDb = await _context.Usuarios
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Nombre == nombreUsuario);

            return usuarioDb != null ? _mapper.Map<sentirsebien_backend.Domain.Entities.Usuario>(usuarioDb) : null;
        }

        public async Task<sentirsebien_backend.Domain.Entities.Usuario> ObtenerPorEmailAsync(string email)
        {
            var usuarioDb = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            return usuarioDb != null ? _mapper.Map<sentirsebien_backend.Domain.Entities.Usuario>(usuarioDb) : null;
        }

        public async Task<IEnumerable<sentirsebien_backend.Domain.Entities.Usuario>> ObtenerTodosAsync()
        {
            var usuariosDb = await _context.Usuarios
                .Include(u => u.Roles)
                .ToListAsync();

            return _mapper.Map<IEnumerable<sentirsebien_backend.Domain.Entities.Usuario>>(usuariosDb);
        }

        // métodos de modificación de usuarios

        public async Task<Result> AgregarAsync(sentirsebien_backend.Domain.Entities.Usuario usuario)
        {
            try
            {
                var usuarioDb = _mapper.Map<sentirsebien_backend.DataAccess.Models.Usuario>(usuario);
                usuarioDb.EsCliente = true;

                await _context.Usuarios.AddAsync(usuarioDb);
                await _context.SaveChangesAsync();
                return Result.Success("Usuario agregado exitosamente.");
            }
            catch (Exception ex)
            {
                return Result.Failure($"Error al agregar el usuario: {ex.Message}");
            }
        }

        public async Task ActualizarAsync(sentirsebien_backend.Domain.Entities.Usuario usuario)
        {
            var usuarioExistente = await _context.Usuarios.FindAsync(usuario.Id);

            if (usuarioExistente != null)
            {
                _mapper.Map(usuario, usuarioExistente);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"No se encontró el usuario con ID {usuario.Id}");
            }
        }

        // otros métodos

        public async Task<string> BuscarContraseñaAsync(int usuarioId)
        {
            var usuario = await ObtenerPorIdAsync(usuarioId);
            return usuario?.Contraseña;
        }
    }
}
