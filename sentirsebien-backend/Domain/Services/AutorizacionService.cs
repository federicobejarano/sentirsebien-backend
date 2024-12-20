﻿using sentirsebien_backend.DataAccess.Repositories;
using sentirsebien_backend.Domain.ValueObjects;
using sentirsebien_backend.Domain.Entities;

namespace sentirsebien_backend.Domain.Services
{
    public class AutorizacionService : IAutorizacionService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IRolRepository _rolRepository;
        private readonly IPermisoRepository _permisoRepository;

        public AutorizacionService(IUsuarioRepository usuarioRepository, IRolRepository rolRepository, IPermisoRepository permisoRepository)
        {
            _usuarioRepository = usuarioRepository;
            _rolRepository = rolRepository;
            _permisoRepository = permisoRepository;
        }

        // método principal : obtener roles y permisos del usuario
        public async Task<DatosDeAutorizacionUsuario> ObtenerAutorizacionUsuarioAsync(Usuario usuario)
        {
            // 1.
            var roles = await ObtenerRolesDeUsuarioAsync(usuario.Id);

            // 2. 
            var permisos = await ObtenerPermisosPorRolesAsync(roles);

            // 3. 
            return new DatosDeAutorizacionUsuario(usuario.Id, roles.ToList(), permisos.ToList());
        }

        // otros métodos

        // obtener roles asociados un usuario
        private async Task<IEnumerable<Rol>> ObtenerRolesDeUsuarioAsync(int usuarioId)
        {
            var roles = await _rolRepository.ObtenerRolesPorUsuario(usuarioId);
            return roles;
        }

        // obtener permisos asociados a cada rol
        private async Task<IEnumerable<Permiso>> ObtenerPermisosPorRolesAsync(IEnumerable<Rol> roles)
        {
            // Extraer los IDs de los roles
            var roleIds = roles.Select(r => r.Id).ToList();

            // Hacer una sola consulta para obtener los permisos asociados a todos los roles
            var permisos = await _permisoRepository.ObtenerPermisosPorRoles(roleIds);

            return permisos;
        }
    }
}
