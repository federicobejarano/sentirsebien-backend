using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace sentirsebien_backend.DataAccess.Models
{
    [Table("Terapeuta")]
    public class Terapeuta
    {
        // clave primaria para Terapeuta, corresponde a la clave de Personal
        [Key]
        public int ID { get; set; }
        [ForeignKey("Personal")]
        public int ID_Personal { get; set; }

        // relación 1:1 con Personal
        public virtual Personal Personal { get; set; }

        // clave foránea hacia la tabla Especialidad (relación N:1)
        [ForeignKey("IdEspecialidad")]
        public int ID_Especialidad { get; set; }

        // public virtual Especialidad Especialidad { get; set; }
    }
}
