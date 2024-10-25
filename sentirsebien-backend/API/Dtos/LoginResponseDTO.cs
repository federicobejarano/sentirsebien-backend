using sentirsebien_backend.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace sentirsebien_backend.Application.DTOs
{
    public class LoginResponseDTO
    {
        public string Token { get; private set; }
        public DateTime FechaCreacion { get; private set; }
        public DateTime FechaExpiracion { get; private set; }
        public int UserId { get; private set; }
        public string Email { get; private set; }
        public IEnumerable<string> Roles { get; private set; }
        public IEnumerable<string> Permisos { get; private set; }

        public LoginResponseDTO(TokenAutenticacion token)
        {
            Token = token.Token;
            FechaCreacion = token.FechaCreacion;
            FechaExpiracion = token.FechaExpiracion;
            UserId = token.UserId;
            Email = token.Email;
            Roles = token.Roles;
            Permisos = token.Permisos;
        }
    }

}
