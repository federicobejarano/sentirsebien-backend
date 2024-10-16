﻿namespace sentirsebien_backend.DataAccess.Models
{
    public class Usuario
    {
        public int Id { get; set; }  // PK
        public string Nombre { get; set; }  // VARCHAR(100)
        public string Apellido { get; set; }  // VARCHAR(100)
        public string Email { get; set; }  // VARCHAR(255)
        public string Telefono { get; set; }  // VARCHAR(20)
        public string Direccion { get; set; }  // VARCHAR(255)
        public bool EsCliente { get; set; }  // BOOLEAN

        // relación N:M con Rol a través de Usuario_Rol
        public virtual ICollection<UsuarioRol> Roles { get; set; }
    }
}
