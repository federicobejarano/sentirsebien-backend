using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace sentirsebien_backend.DataAccess.Models
{
    Table["Usuario"]
    public class Usuario
    {
        [Key]
        public int ID { get; set; }  // PK
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
