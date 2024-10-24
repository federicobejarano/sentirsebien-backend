using sentirsebien_backend.Domain.Entities;

namespace sentirsebien_backend.Domain.ValueObjects
{
    public class DatosDeAutorizacionUsuario
    {
        public int UserId { get; }
        public IReadOnlyCollection<Rol> Roles { get; }
        public IReadOnlyCollection<Permiso> Permisos { get; }

        public DatosDeAutorizacionUsuario(int userId, IReadOnlyCollection<Rol> roles, IReadOnlyCollection<Permiso> permisos)
        {
            UserId = userId;
            Roles = roles ?? throw new ArgumentNullException(nameof(roles));
            Permisos = permisos ?? throw new ArgumentNullException(nameof(permisos));
        }
    }
}