namespace sentirsebien_backend.API.Dtos
{
    public record class Cliente
    {
        public int ID { get; set; }
        public int IDUsuario { get; set; } // FK de Usuario

        // Relación 1:1 con Usuario
        public required GetUsuarioDto Usuario { get; set; }
    }
}
