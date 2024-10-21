using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace sentirsebien_backend.DataAccess.Models
{
    [Table("Personal")]
    public class Personal
    {
        [Key]
        public int ID { get; set; }  // PK

        [ForeignKey("Usuario")]
        public int ID_Usuario { get; set; }  // FK y UK, relación 1:1 con Usuario
        public decimal SalarioTotal { get; set; }  // DECIMAL(10,2)
        public virtual Usuario Usuario { get; set; }
    }
}
