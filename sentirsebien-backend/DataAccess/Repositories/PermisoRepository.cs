using AutoMapper;
using Microsoft.EntityFrameworkCore;
using sentirsebien_backend.DataAccess.DbContexts;

namespace sentirsebien_backend.DataAccess.Repositories
{
    public class PermisoRepository : IPermisoRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PermisoRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<sentirsebien_backend.Domain.Entities.Permiso>> ObtenerPermisosPorRol(int rolId)
        {
            var permisos = await _context.RolPermisos
                .Where(rp => rp.ID_Rol == rolId)
                .Include(rp => rp.Permiso)
                .Select(rp => rp.Permiso)
                .ToListAsync();

            return _mapper.Map<List<sentirsebien_backend.Domain.Entities.Permiso>>(permisos);
        }

        public async Task<List<sentirsebien_backend.Domain.Entities.Permiso>> ObtenerPermisosPorUsuario(int usuarioId)
        {
            var rolIds = await _context.UsuarioRoles
                .Where(ur => ur.ID_Usuario == usuarioId)
                .Select(ur => ur.ID_Rol)
                .ToListAsync();

            return await ObtenerPermisosPorRoles(rolIds);
        }

        public async Task<List<sentirsebien_backend.Domain.Entities.Permiso>> ObtenerPermisosPorRoles(IEnumerable<int> roleIds)
        {
            var permisos = await _context.RolPermisos
                .Where(rp => roleIds.Contains(rp.ID_Rol))
                .Include(rp => rp.Permiso)
                .Select(rp => rp.Permiso)
                .Distinct()
                .ToListAsync();

            return _mapper.Map<List<sentirsebien_backend.Domain.Entities.Permiso>>(permisos);
        }

        public sentirsebien_backend.Domain.Entities.Permiso ObtenerPermisoPorTipo(string tipo)
        {
            var permiso = _context.Permisos
                .FirstOrDefault(p => p.TipoPermiso == tipo);

            return _mapper.Map<sentirsebien_backend.Domain.Entities.Permiso>(permiso);
        }

        public void CrearPermiso(sentirsebien_backend.Domain.Entities.Permiso permiso)
        {
            var permisoModel = _mapper.Map<sentirsebien_backend.DataAccess.Models.Permiso>(permiso);
            _context.Permisos.Add(permisoModel);
            _context.SaveChanges();
        }

        public void ActualizarPermiso(sentirsebien_backend.Domain.Entities.Permiso permiso)
        {
            var permisoModel = _mapper.Map<sentirsebien_backend.DataAccess.Models.Permiso> (permiso);
            _context.Permisos.Update(permisoModel);
            _context.SaveChanges();
        }

        public void EliminarPermiso(string tipo)
        {
            var permiso = _context.Permisos
                .FirstOrDefault(p => p.TipoPermiso == tipo);

            if (permiso != null)
            {
                _context.Permisos.Remove(permiso);
                _context.SaveChanges();
            }
        }
    }
}
