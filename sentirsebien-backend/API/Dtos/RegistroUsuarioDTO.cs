using System.ComponentModel.DataAnnotations;

namespace sentirsebien_backend.API.Dtos
{
    public class RegistroUsuarioDTO
    {
        /* 
         * 
         * Propiedades como Nombre, Apellido, Email, Telefono y Contraseña son
         * requeridas, mientras que Direccion es opcional.
         * 
         * EmailAddress es una anotación de datos que valida automáticamente
         * el formato del email, y MinLength asegura que la contraseña tenga
         * al menos 6 caracteres.
         * 
         */

        // Nombre del usuario, es obligatorio
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; }

        // Apellido del usuario, es obligatorio
        [Required(ErrorMessage = "El apellido es obligatorio.")]
        public string Apellido { get; set; }

        // Email del usuario, es obligatorio y se valida su formato
        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "Formato de email inválido.")]
        public string Email { get; set; }

        // Teléfono del usuario, es obligatorio
        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        public string Telefono { get; set; }

        // Dirección del usuario, es opcional en este caso
        public string Direccion { get; set; }

        // Contraseña del usuario, es obligatorio y debe tener un mínimo de 6 caracteres
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        public string Contraseña { get; set; }
    }
}
