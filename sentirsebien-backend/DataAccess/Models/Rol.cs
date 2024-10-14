namespace sentirsebien_backend.DataAccess.Models
{
    public class Rol
    {
        public int ID { get; set; }  // PK
        public string Codigo { get; set; }  // VARCHAR(10)
        public string Nombre { get; set; }  // VARCHAR(50)
        public string Area { get; set; }  // VARCHAR(50)
        public decimal SalarioRol { get; set; }  // DECIMAL(10,2)

        // relación N:M con Permiso a través de RolPermiso
        public virtual ICollection<RolPermiso> Permisos { get; set; }

        // relación N:M con Usuario a través de Usuario_Rol
        public virtual ICollection<UsuarioRol> Usuarios { get; set; }
    }

}
