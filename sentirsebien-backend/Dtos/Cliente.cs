namespace sentirsebien_backend.Dtos
{
    public class Cliente
    {
        public int ID { get; set; }
        public int IDUsuario { get; set; } // FK de Usuario

        // Relación 1:1 con Usuario
        public Usuario Usuario { get; set; }
    }
}
