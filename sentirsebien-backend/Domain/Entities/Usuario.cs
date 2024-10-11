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
        private string contraseña;

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
            get { return contraseña; }
            set { contraseña = value; }
        }

        public Usuario(int id, string nombre, string email, string contraseña)
        {
            this.id = id;
            this.nombre = nombre;
            this.email = email;
            this.contraseña = contraseña;
        }

        // Métodos adicionales para autenticación y manejo de usuario
        public bool ValidarContraseña(string contraseñaIngresada)
        {
            return contraseña == contraseñaIngresada;
        }

        public override string ToString()
        {
            return $"Usuario: {nombre} (Email: {email})";
        }
    }

}
