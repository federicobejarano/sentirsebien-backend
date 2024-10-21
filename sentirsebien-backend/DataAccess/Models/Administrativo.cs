using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace sentirsebien_backend.DataAccess.Models
{
    [Table("Administrativo")]
    public class Administrativo
    {
        // clave primaria para Administrativo, corresponde a la clave de Personal
        [Key]
        public int ID { get; set; }

        [ForeignKey("Personal")]
        public int ID_Personal { get; set; }

        // relación 1:1 con Personal
        public virtual Personal Personal { get; set; }
    }
}
