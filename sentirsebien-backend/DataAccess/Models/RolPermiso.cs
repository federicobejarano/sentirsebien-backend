using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace sentirsebien_backend.DataAccess.Models
{
    [Table("RolPermiso")]
    public class RolPermiso
    {
        [Key]
        public int IdRolPermiso { get; set; }  // PK

        [ForeignKey("Rol")]
        public int ID_Rol { get; set; }  // FK a Rol.
        [ForeignKey("Permiso")]
        public int ID_Permiso { get; set; }  // FK a Permiso

        public virtual Rol Rol { get; set; }
        public virtual Permiso Permiso { get; set; }
    }
}
