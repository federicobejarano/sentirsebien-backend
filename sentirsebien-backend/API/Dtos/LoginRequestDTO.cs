using System.ComponentModel.DataAnnotations;

namespace sentirsebien_backend.API.Dtos
{
    public class LoginRequestDTO
    {
        // email del usuario
        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del email no es válido.")]
        [MaxLength(100, ErrorMessage = "El email no debe exceder los 100 caracteres.")]
        public string Email { get; set; }

        // contraseña del usuario
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        [MaxLength(50, ErrorMessage = "La contraseña no debe exceder los 50 caracteres.")]
        public string Password { get; set; }

        // constructor con validaciones básicas
        public LoginRequestDTO(string email, string password)
        {
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Password = password ?? throw new ArgumentNullException(nameof(password));
        }
    }
}
