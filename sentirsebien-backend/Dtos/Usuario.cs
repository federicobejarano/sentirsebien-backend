namespace sentirsebien_backend.Dtos
{
    public class Usuario
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public bool EsCliente { get; set; }

        // Relación 1:1 con Cliente o Personal (dependiendo de EsCliente)
        public Cliente Cliente { get; set; }
        public Personal Personal { get; set; }

        public Usuario(int id, string nombre, string apellido, string email, string telefono, string direccion, bool esCliente)
        {
            ID = id;
            Nombre = nombre;
            Apellido = apellido;
            Email = email;
            Telefono = telefono;
            Direccion = direccion;
            EsCliente = esCliente;
        }

        public string ToString()
        {
            return $"Usuario {ID}\n\tNombre: {Apellido}, {Nombre} \nContacto\tEmail: {Email} \n\tTeléfono{Telefono} \n\tDirección: {Direccion}";
        }
    }
}
