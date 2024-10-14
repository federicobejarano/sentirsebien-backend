namespace sentirsebien_backend.DataAccess.Models
{
    public class Cliente
    {
        public int ID { get; set; }  // PK
        public int ID_Usuario { get; set; }  // FK y UK, relación 1:1 con Usuario
        public virtual Usuario Usuario { get; set; }
    }
}
