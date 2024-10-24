namespace sentirsebien_backend.Domain.ValueObjects
{
    public class DatosDeAutenticacionUsuario
    {
        public int UserId { get; }
        public string Email { get; }

        public DatosDeAutenticacionUsuario(int userId, string email)
        {
            UserId = userId;
            Email = email ?? throw new ArgumentNullException(nameof(email));
        }
    }
}
