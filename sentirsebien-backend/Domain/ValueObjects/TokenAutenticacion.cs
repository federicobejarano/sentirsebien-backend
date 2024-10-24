using System;

namespace sentirsebien_backend.Domain.ValueObjects
{
    public class TokenAutenticacion
    {
        public string Token { get; private set; }
        public DateTime FechaCreacion { get; private set; }
        public DateTime FechaExpiracion { get; private set; }
        public int UserId { get; private set; }
        public string Email { get; private set; }
        public IReadOnlyCollection<string> Roles { get; private set; }
        public IReadOnlyCollection<string> Permisos { get; private set; }

        public TokenAutenticacion(
            string token,
            DateTime fechaCreacion,
            DateTime fechaExpiracion,
            int userId,
            string email,
            IReadOnlyCollection<string> roles,
            IReadOnlyCollection<string> permisos)
        {
            Token = token ?? throw new ArgumentNullException(nameof(token));
            FechaCreacion = fechaCreacion;
            FechaExpiracion = fechaExpiracion;
            UserId = userId;
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Roles = roles ?? throw new ArgumentNullException(nameof(roles));
            Permisos = permisos ?? throw new ArgumentNullException(nameof(permisos));
        }

        public bool EsValido()
        {
            return DateTime.UtcNow < FechaExpiracion;
        }

        public bool EstaExpirado()
        {
            return DateTime.UtcNow >= FechaExpiracion;
        }
    }
}
