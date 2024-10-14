using sentirsebien_backend.Domain.Entities;

namespace sentirsebien_backend.DataAccess.Models
{
    public class RolPermiso
    {
        public int IdRolPermiso { get; set; }  // PK
        public int ID_Rol { get; set; }  // FK a Rol
        public int ID_Permiso { get; set; }  // FK a Permiso

        public virtual Rol Rol { get; set; }
        public virtual Permiso Permiso { get; set; }
    }
}
