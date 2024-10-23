using AutoMapper;
using Microsoft.EntityFrameworkCore;
using sentirsebien_backend.DataAccess.DbContexts;
using sentirsebien_backend.DataAccess.Models;
using sentirsebien_backend.Domain.Entities;
using sentirsebien_backend.Domain.Services;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace sentirsebien_backend.DataAccess.Repositories
{
    public class RolRepository // : IRolRepository
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
            var rol = _context.Roles.Find(id); // entre 1 y 5
            return _mapper.Map<sentirsebien_backend.Domain.Entities.Rol>(rol);
        }


        public async Task<sentirsebien_backend.Domain.Entities.Rol> GetByNombreAsync(string nombreRol)
        {
            var rol = await _context.Roles
                .Include(r => r.Permisos) // Incluir la lista de permisos
                .FirstOrDefaultAsync(r => r.Nombre == nombreRol); // Buscar por nombre
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
                .ThenInclude(ur => ur.Rol) // revisar
                .FirstOrDefault(u => u.Id == usuarioId);

            return _mapper.Map<List<sentirsebien_backend.Domain.Entities.Rol>>(usuario?.Roles.Select(ur => ur.Rol).ToList());
        }

        // Obtener permisos por Rol, asegurando que el tipo coincida.
        public List<sentirsebien_backend.Domain.Entities.Permiso> ObtenerPermisosPorRol(int rolId)
        {
            var rol = ObtenerRolPorId(rolId);
            if (rol == null)
            {
                throw new Exception("Rol no encontrado.");
            }

            if (SistemaPermisos.PermisosPorRol.TryGetValue(rol.Tipo, out List<sentirsebien_backend.Domain.Entities.Permiso> permisos))
            {
                return permisos;
            }

            throw new Exception("No se encontraron permisos para el rol especificado.");
        }

        /*
        public List<sentirsebien_backend.DataAccess.Models.Permiso> ObtenerPermisosPorRol(int rolId)
        {
            return _context.RolPermisos
                .Where(rp => rp.ID_Rol == rolId)
                .Select(rp => rp.Permiso)
                .ToList();
        }
        */
    }
}
