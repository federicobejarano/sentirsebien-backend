using sentirsebien_backend.Domain.Entities;

namespace sentirsebien_backend.Domain.ValueObjects
{
    public class AutorizacionUsuario
    {
        public int UserId { get; }
        public string Email { get; }
        public IReadOnlyCollection<Rol> Roles { get; }
        public IReadOnlyCollection<Permiso> Permisos { get; }

        public AutorizacionUsuario(int userId, string email, IReadOnlyCollection<Rol> roles, IReadOnlyCollection<Permiso> permisos)
        {
            UserId = userId;
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Roles = roles ?? throw new ArgumentNullException(nameof(roles));
            Permisos = permisos ?? throw new ArgumentNullException(nameof(permisos));
        }
    }
}
