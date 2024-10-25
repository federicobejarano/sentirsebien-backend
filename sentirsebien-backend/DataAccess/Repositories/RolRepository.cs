using AutoMapper;
using Microsoft.EntityFrameworkCore;
using sentirsebien_backend.DataAccess.DbContexts;
using sentirsebien_backend.Domain.Entities;
using sentirsebien_backend.DataAccess.Models;
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

        public async Task<sentirsebien_backend.Domain.Entities.Rol> GetByNombreAsync(string nombreRol)
        {
            var rol = await _context.Roles
                .Include(r => r.Permisos)  // Incluye permisos relacionados
                .FirstOrDefaultAsync(r => r.Nombre == nombreRol);

            return _mapper.Map<sentirsebien_backend.Domain.Entities.Rol>(rol);
        }

        public void CrearRol(sentirsebien_backend.Domain.Entities.Rol rol)
        {
            var rolEntidad = _mapper.Map<sentirsebien_backend.DataAccess.Models.Rol>(rol);
            _context.Roles.Add(rolEntidad);
            _context.SaveChanges();
        }

        public void ActualizarRol(sentirsebien_backend.Domain.Entities.Rol rol)
        {
            var rolExistente = _context.Roles.Find(rol.Id);
            if (rolExistente != null)
            {
                _mapper.Map(rol, rolExistente);
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

        public async Task<List<sentirsebien_backend.Domain.Entities.Rol>> ObtenerTodosLosRoles()
        {
            var roles = await _context.Roles.ToListAsync();
            return _mapper.Map<List<sentirsebien_backend.Domain.Entities.Rol>>(roles);
        }

        public async Task<List<sentirsebien_backend.Domain.Entities.Rol>> ObtenerRolesPorUsuario(int usuarioId)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.Roles)
                .ThenInclude(ur => ur.Rol)
                .FirstOrDefaultAsync(u => u.ID == usuarioId);

            if (usuario == null)
                return new List<sentirsebien_backend.Domain.Entities.Rol>();

            var rolesDeUsuario = usuario.Roles.Select(ur => ur.Rol).ToList();
            return _mapper.Map<List<sentirsebien_backend.Domain.Entities.Rol>>(rolesDeUsuario);
        }

        public List<sentirsebien_backend.Domain.Entities.Permiso> ObtenerPermisosPorRol(int rolId)
        {
            var rol = _context.Roles
                .Include(r => r.Permisos)
                .FirstOrDefault(r => r.ID == rolId);

            if (rol == null)
            {
                throw new Exception("Rol no encontrado.");
            }

            return _mapper.Map<List<sentirsebien_backend.Domain.Entities.Permiso>>(rol.Permisos.Select(rp => rp.Permiso).ToList());
        }
    }
}
