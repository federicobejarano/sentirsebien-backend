using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace sentirsebien_backend.DataAccess.Models
{
    [Table("Cliente")]
    public class Cliente
    {
        [Key]
        public int ID { get; set; }  // PK

        [ForeignKey("Usuario")]
        public int ID_Usuario { get; set; }  // FK y UK, relación 1:1 con Usuario
        public virtual Usuario Usuario { get; set; }
    }
}
