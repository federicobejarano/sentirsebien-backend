using sentirsebien_backend.Domain.Entities;

namespace sentirsebien_backend.Domain.Services
{
    public interface IGestorRolesService
    {
        /*
         * Nombres de los roles disponibles: 
         * 
         * "Administrador"
         * "Recepcionista"
         * "Gerente"
         * "Especialista" (terapeuta)
         * "Cliente" (rol por defecto)
         * 
         */

        // asignar un rol a un usuario
        public Task AsignarRol(sentirsebien_backend.Domain.Entities.Usuario usuario, string nombreRol);

        // eliminar un rol de un usuario
        // void EliminarRol(int usuarioId, string nombreRol); // implementación: verificar si el usuario tiene ese rol
        
        // obtener la lista de roles asignados a un usuario
        List<Rol> ObtenerRolesPorUsuario(int usuarioId);

        /********** otros métodos **********/

        // buscar un rol del Spa -> asignación de roles en GestorRolesService (capa de dominio)

        // sentirsebien_backend.Domain.Entities.Rol ObtenerRolPorNombre(string nombreRol);


    }
}
