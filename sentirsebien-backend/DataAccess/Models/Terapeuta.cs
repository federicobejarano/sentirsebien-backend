using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace sentirsebien_backend.DataAccess.Models
{
    public class Terapeuta
    {
        // clave primaria para Terapeuta, corresponde a la clave de Personal
        [Key]
        [ForeignKey("Personal")]
        public int Id { get; set; }

        // relación 1:1 con Personal
        public virtual Personal Personal { get; set; }

        // clave foránea hacia la tabla Especialidad (relación N:1)
        public int IdEspecialidad { get; set; }
        
        //[ForeignKey("IdEspecialidad")]

        // public virtual Especialidad Especialidad { get; set; }
    }
}
