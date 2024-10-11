using Microsoft.AspNetCore.Identity;

namespace sentirsebien_backend.Domain.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly PasswordHasher<object> _passwordHasher;

        public PasswordService()
        {
            _passwordHasher = new PasswordHasher<object>();
        }

        // generar el hash de la contraseña
        public string HashPassword(string password)
        {
            // utilizar PasswordHasher<T> para generar el hash con un salt
            return _passwordHasher.HashPassword(null, password);
        }

        // verificar si la contraseña ingresada coincide con el hash almacenado
        public bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}
