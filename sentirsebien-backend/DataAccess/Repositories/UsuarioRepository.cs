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
        public sentirsebien_backend.Domain.Entities.Usuario ObtenerPorId(int usuarioId)
        {
            var usuarioDb = _context.Usuarios
                .Include(u => u.Roles) // Incluir roles relacionados
                .FirstOrDefault(u => u.Id == usuarioId);

            if (usuarioDb == null)
            {
                return null; // o lanzar una excepción si lo prefieres
            }

            // Mapear la entidad de acceso a datos a la entidad de dominio
            return _mapper.Map<sentirsebien_backend.Domain.Entities.Usuario>(usuarioDb);
        }

        // btener usuario por nombre de usuario
        public sentirsebien_backend.Domain.Entities.Usuario ObtenerPorNombreUsuario(string nombreUsuario)
        {
            var usuarioDb = _context.Usuarios
                .Include(u => u.Roles) // incluir roles relacionados
                .FirstOrDefault(u => u.Nombre == nombreUsuario);

            if (usuarioDb == null)
            {
                return null;
            }

            return _mapper.Map<sentirsebien_backend.Domain.Entities.Usuario>(usuarioDb);
        }

        // obtener usuario por nombre de usuario
        public sentirsebien_backend.Domain.Entities.Usuario ValidarEmail(string email)
        {
            var usuarioDb = _context.Usuarios
                //.Include(u => u.Roles) // incluir roles relacionados
                .FirstOrDefault(u => u.Email == email);

            if (usuarioDb == null)
            {
                return null;
            }

            return _mapper.Map<sentirsebien_backend.Domain.Entities.Usuario>(usuarioDb);
        }

        // obtener todos los usuarios
        public IEnumerable<sentirsebien_backend.Domain.Entities.Usuario> ObtenerTodos()
        {
            var usuariosDb = _context.Usuarios
                .Include(u => u.Roles) // incluir roles relacionados
                .ToList();

            // mapear lista de entidades de acceso a datos a la capa de dominio
            return _mapper.Map<IEnumerable<sentirsebien_backend.Domain.Entities.Usuario>>(usuariosDb);
        }

        // agregar un nuevo usuario
        public Result Agregar(sentirsebien_backend.Domain.Entities.Usuario usuario)
        {
            try
            {
                // mapear entidad de dominio Usuario a entidad de BD
                var usuarioDb = _mapper.Map<sentirsebien_backend.DataAccess.Models.Usuario>(usuario);

                // Agregar y guardar cambios en la base de datos
                _context.Usuarios.Add(usuarioDb);
                _context.SaveChanges();
                return Result.Success("Usuario agregado exitosamente.");
            }
            catch (Exception ex)
            {
                // manejar error (acá se puede hacer logging de ser necesario)
                return Result.Failure("Error al agregar el usuario.");
            }
        }


        // actualizar un usuario existente
        public void Actualizar(sentirsebien_backend.Domain.Entities.Usuario usuario)
        {
            var usuarioExistente = _context.Usuarios.Find(usuario.Id);

            if (usuarioExistente != null)
            {
                // mapear los cambios desde la entidad de dominio a la entidad de datos
                _mapper.Map(usuario, usuarioExistente);

                // guardar los cambios en la base de datos
                _context.SaveChanges();
            }
            else
            {
                // manejar el caso en que el usuario no exista (retornar un error, excepción, etc.)
                throw new KeyNotFoundException($"No se encontró el usuario con ID {usuario.Id}");
            }
        }

        // eliminar un usuario por ID
        public void Eliminar(int id)
        {
            var usuario = _context.Usuarios.Find(id);

            if (usuario != null)
            {
                // remover el usuario y guardar cambios
                _context.Usuarios.Remove(usuario);
                _context.SaveChanges();
            }
            else
            {
                // manejar el caso en que el usuario no exista
                throw new KeyNotFoundException($"No se encontró el usuario con ID {id}");
            }
        }
    }
}
