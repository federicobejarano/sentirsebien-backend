using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace sentirsebien_backend.DataAccess.Models
{
    public class Administrativo
    {
        // clave primaria para Administrativo, corresponde a la clave de Personal
        [Key]
        [ForeignKey("Personal")]
        public int Id { get; set; }

        // relación 1:1 con Personal
        public virtual Personal Personal { get; set; }
    }
}
