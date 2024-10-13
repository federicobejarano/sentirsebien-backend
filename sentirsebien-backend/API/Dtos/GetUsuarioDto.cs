namespace sentirsebien_backend.API.Dtos
{
    public record GetUsuarioDto(int ID, string Nombre, string Apellido, string Email, string Telefono, string Direccion, bool EsCliente)
    {
        /*
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public bool EsCliente { get; set; }
        */

        // Relación 1:1 con Cliente o Personal (dependiendo de EsCliente)
        public Cliente? Cliente { get; set; }
        public GetPersonalDto? Personal { get; set; }


    }
}
