using AutoMapper;
using Microsoft.EntityFrameworkCore;
using sentirsebien_backend.DataAccess.DbContexts;
using sentirsebien_backend.DataAccess.Models; // para referenciar el modelo de acceso a datos
using sentirsebien_backend.Domain.Entities;   // para referenciar la entidad de dominio
using sentirsebien_backend.Domain.Shared;
using System.Collections.Generic;
using System.Linq;

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

        // obtener usuario por ID (int)
        public async Task<sentirsebien_backend.Domain.Entities.Usuario> ObtenerPorIdAsync(int usuarioId)
        {
            var usuarioDb = await _context.Usuarios
                .Include(u => u.Roles) // incluir roles relacionados
                .FirstOrDefaultAsync(u => u.ID == usuarioId);

            if (usuarioDb == null)
            {
                return null; // o lanzar una excepción si lo prefieres
            }

            // mapear entidad de acceso a datos a la entidad de dominio
            return _mapper.Map<sentirsebien_backend.Domain.Entities.Usuario>(usuarioDb);
        }

        // btener usuario por nombre de usuario
        public async Task<sentirsebien_backend.Domain.Entities.Usuario> ObtenerPorNombreUsuarioAsync(string nombreUsuario)
        {
            var usuarioDb = await _context.Usuarios
                .Include(u => u.Roles) // incluir roles relacionados
                .FirstOrDefaultAsync(u => u.Nombre == nombreUsuario);

            if (usuarioDb != null) return _mapper.Map<sentirsebien_backend.Domain.Entities.Usuario>(usuarioDb);

            else return null;
        }

        // obtener usuario por nombre de usuario
        public async Task<sentirsebien_backend.Domain.Entities.Usuario> ObtenerPorEmailAsync(string email)
        {
            var usuarioDb = await _context.Usuarios
                .Include(u => u.Roles) // incluir roles relacionados
                .FirstOrDefaultAsync(u => u.Email == email);

            if (usuarioDb != null) return _mapper.Map<sentirsebien_backend.Domain.Entities.Usuario>(usuarioDb);

            else return null;
        }

        // obtener usuario por nombre de usuario
        public async Task<sentirsebien_backend.Domain.Entities.Usuario> ValidarEmail(string email)
        {
            // buscar usuario por email de manera asincrónica en la base de datos
            var usuarioDb = await _context.Usuarios
                //.Include(u => u.Roles) // descomentar si es necesario incluir los roles relacionados
                .FirstOrDefaultAsync(u => u.Email == email);

            if (usuarioDb != null) return _mapper.Map<sentirsebien_backend.Domain.Entities.Usuario>(usuarioDb);

            else return null;
        }

        // obtener todos los usuarios
        public async Task<IEnumerable<sentirsebien_backend.Domain.Entities.Usuario>> ObtenerTodosAsync()
        {
            var usuariosDb = await _context.Usuarios
                .Include(u => u.Roles) // incluir roles relacionados
                .ToListAsync();

            return _mapper.Map<IEnumerable<sentirsebien_backend.Domain.Entities.Usuario>>(usuariosDb);
        }

        // agregar un nuevo usuario
        public async Task<Result> AgregarAsync(sentirsebien_backend.Domain.Entities.Usuario usuario)
        {
            try
            {
                // mapear entidad de dominio Usuario a entidad de BD
                var usuarioDb = _mapper.Map<sentirsebien_backend.DataAccess.Models.Usuario>(usuario);

                usuarioDb.EsCliente = true;

                // Agregar y guardar cambios en la base de datos de manera asíncrona
                await _context.Usuarios.AddAsync(usuarioDb);
                await _context.SaveChangesAsync();
                return Result.Success("Usuario agregado exitosamente.");
            }
            catch (Exception ex)
            {
                // manejar error (acá se puede hacer logging de ser necesario)
                return Result.Failure("Error al agregar el usuario.");
            }
        }


        // actualizar un usuario existente
        public async Task ActualizarAsync(sentirsebien_backend.Domain.Entities.Usuario usuario)
        {
            var usuarioExistente = await _context.Usuarios.FindAsync(usuario.Id);

            if (usuarioExistente != null)
            {
                // mapear los cambios desde la entidad de dominio a la entidad de datos
                _mapper.Map(usuario, usuarioExistente);

                // guardar los cambios en la base de datos de manera asíncrona
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"No se encontró el usuario con ID {usuario.Id}");
            }
        }

        // eliminar un usuario por ID - versión asíncrona
        public async Task EliminarAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"No se encontró el usuario con ID {id}");
            }
        }
    }
}
