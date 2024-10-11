using sentirsebien_backend.Domain.Services;

namespace sentirsebien_backend.Domain.Entities
{
    public class Usuario
    {
        /* 
         * entidad para:
         * 
         * * autenticación de usuario
         * * validación de contraseñas
         * * relación entre tipos de usuario (entidades Cliente y Personal)
         * 
        */

        private int id;
        private string nombre;
        private string email;

        // private string contraseña;
        private string hashContraseña; // almacenar hash en lugar de contraseña

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string NombreCompleto
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Contraseña
        {
            get { return hashContraseña; }
            set { hashContraseña = value; }
        }

        public Usuario(int id, string nombre, string email, string contraseña, IPasswordService passwordService)
        {
            this.id = id;
            this.nombre = nombre;
            this.email = email;

            // this.contraseña = contraseña;
            this.hashContraseña = passwordService.HashPassword(contraseña); // hashear la contraseña antes de almacenarla
        }

        // verificación de contraseña
        public bool VerifyPassword(string providedPassword, IPasswordService passwordService)
        {
            return passwordService.VerifyPassword(this.hashContraseña, providedPassword);
        }

        // modificación de la contraseña
        public void UpdatePassword(string newPassword, IPasswordService passwordService)
        {
            this.hashContraseña = passwordService.HashPassword(newPassword);
        }

        public override string ToString()
        {
            return $"Usuario: {nombre} (Email: {email})";
        }
    }

}
