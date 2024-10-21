using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace sentirsebien_backend.DataAccess.Models
{
    [Table("Especialidad")]
    public class Especialidad
    {
        [Key]
        public int ID { get; set; }

        // código de la especialidad, clave alternativa
        [Required]
        [MaxLength(10)]
        public string Codigo { get; set; }

        // nombre de la especialidad, con restricción opcional de longitud mínima
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }
    }
}
