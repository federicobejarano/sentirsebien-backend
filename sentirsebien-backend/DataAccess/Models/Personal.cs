namespace sentirsebien_backend.DataAccess.Models
{
    public class Personal
    {
        public int ID { get; set; }  // PK
        public int ID_Usuario { get; set; }  // FK y UK, relación 1:1 con Usuario
        public decimal SalarioTotal { get; set; }  // DECIMAL(10,2)
        public virtual Usuario Usuario { get; set; }
    }
}
