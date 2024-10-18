using sentirsebien_backend.Domain.Services;
using System.Collections.Generic;

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
        private string apellido;
        private string email;
        private string hashContraseña; // almacenar hash en lugar de contraseña
        private HashSet<sentirsebien_backend.Domain.Entities.Rol> roles;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string Apellido
        {
            get { return apellido; }
            set { apellido = value; }
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

        public HashSet<sentirsebien_backend.Domain.Entities.Rol> Roles
        {
            get { return roles; }
            set { roles = value; }
        }

        public Usuario (string nombre, string apellido, string email, string hashContraseña)
        {
            this.nombre = nombre;
            this.email = email;
            this.hashContraseña = hashContraseña;
            this.roles = new HashSet<sentirsebien_backend.Domain.Entities.Rol>(); // colección vacía
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

        public void AsignarRol(sentirsebien_backend.Domain.Entities.Rol nuevoRol)
        {
            roles.Add(nuevoRol);
        }
    }
}
