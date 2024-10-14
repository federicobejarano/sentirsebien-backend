using sentirsebien_backend.Domain.Entities;

namespace sentirsebien_backend.DataAccess.Models
{
    public class UsuarioRol
    {
        public int ID_Usuario { get; set; }  // FK a Usuario
        public int ID_Rol { get; set; }  // FK a Rol
        public DateTime FechaInicio { get; set; }  // DATE
        public int HorasSemana { get; set; }  // INT

        public virtual Usuario Usuario { get; set; }
        public virtual Rol Rol { get; set; }
    }

}
