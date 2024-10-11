namespace sentirsebien_backend.Domain.Services
{
    public interface IPasswordService
    {
        string HashPassword(string password);  // hash de la contraseña
        bool VerifyPassword(string hashedPassword, string providedPassword); // verificación de coincidencia con el hash
    }
}
