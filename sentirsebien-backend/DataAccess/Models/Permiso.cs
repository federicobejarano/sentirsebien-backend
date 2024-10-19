using System.ComponentModel.DataAnnotations;

namespace sentirsebien_backend.DataAccess.Models
{
    public class Permiso
    {
        [Key]
        public int IdPermiso { get; set; }  // PK
        public string Codigo { get; set; }  // VARCHAR(10)
        public string TipoPermiso { get; set; }  // VARCHAR(50)
        public string AccionPermiso { get; set; }  // VARCHAR(50)

        // relación N:M con Rol a través de RolPermiso
        public virtual ICollection<RolPermiso> Roles { get; set; }
    }
}
