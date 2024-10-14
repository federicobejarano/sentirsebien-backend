using AutoMapper;
using Microsoft.EntityFrameworkCore;
using sentirsebien_backend.DataAccess.DbContexts;
using sentirsebien_backend.DataAccess.Models;
using sentirsebien_backend.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sentirsebien_backend.DataAccess.Repositories
{
    public class RolRepository : IRolRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RolRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public sentirsebien_backend.Domain.Entities.Rol ObtenerRolPorId(int id)
        {
            var rol = _context.Roles.Find(id);
            return _mapper.Map<sentirsebien_backend.Domain.Entities.Rol>(rol);
        }

        public sentirsebien_backend.Domain.Entities.Rol ObtenerRolPorNombre(string nombre)
        {
            var rol = _context.Roles.FirstOrDefault(r => r.Nombre == nombre);
            return _mapper.Map<sentirsebien_backend.Domain.Entities.Rol>(rol);
        }

        public void CrearRol(sentirsebien_backend.Domain.Entities.Rol rol)
        {
            var rolModel = _mapper.Map<sentirsebien_backend.DataAccess.Models.Rol>(rol);
            _context.Roles.Add(rolModel);
            _context.SaveChanges();
        }

        public void ActualizarRol(sentirsebien_backend.Domain.Entities.Rol rol)
        {
            var rolExistente = _context.Roles.Find(rol.Id);
            if (rolExistente != null)
            {
                _mapper.Map(rol, rolExistente); // mapear la entidad de dominio sobre la entidad existente
                _context.Roles.Update(rolExistente);
                _context.SaveChanges();
            }
        }

        public void EliminarRol(int id)
        {
            var rol = _context.Roles.Find(id);
            if (rol != null)
            {
                _context.Roles.Remove(rol);
                _context.SaveChanges();
            }
        }

        public List<sentirsebien_backend.Domain.Entities.Rol> ObtenerTodosLosRoles()
        {
            var roles = _context.Roles.ToList();
            return _mapper.Map<List<sentirsebien_backend.Domain.Entities.Rol>>(roles);
        }

        public List<sentirsebien_backend.Domain.Entities.Rol> ObtenerRolesPorUsuario(int usuarioId)
        {
            var usuario = _context.Usuarios
                .Include(u => u.Roles)
                .ThenInclude(ur => ur.Rol)
                .FirstOrDefault(u => u.ID == usuarioId);

            return _mapper.Map<List<sentirsebien_backend.Domain.Entities.Rol>>(usuario?.Roles.Select(ur => ur.Rol).ToList());
        }

        public void AsignarRolAUsuario(int usuarioId, sentirsebien_backend.Domain.Entities.Rol rol)
        {
            var usuario = _context.Usuarios.Include(u => u.Roles).FirstOrDefault(u => u.ID == usuarioId);
            if (usuario != null)
            {
                var rolEntity = _context.Roles.FirstOrDefault(r => r.ID == rol.Id);
                if (rolEntity != null && !usuario.Roles.Any(ur => ur.ID_Rol == rolEntity.ID))
                {
                    usuario.Roles.Add(new UsuarioRol { ID_Usuario = usuario.ID, ID_Rol = rolEntity.ID });
                    _context.SaveChanges();
                }
            }
        }

        public void EliminarRolDeUsuario(int usuarioId, sentirsebien_backend.Domain.Entities.Rol rol)
        {
            var usuario = _context.Usuarios.Include(u => u.Roles).FirstOrDefault(u => u.ID == usuarioId);
            if (usuario != null)
            {
                var rolAEliminar = usuario.Roles.FirstOrDefault(ur => ur.ID_Rol == rol.Id);
                if (rolAEliminar != null)
                {
                    usuario.Roles.Remove(rolAEliminar);
                    _context.SaveChanges();
                }
            }
        }
    }
}
