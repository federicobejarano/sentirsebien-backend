namespace sentirsebien_backend.Domain.Entities
{
    public class Cliente : Usuario
    {
        private string direccion;
        private string telefono;

        public string Direccion
        {
            get { return direccion; }
            set { direccion = value; }
        }

        public string Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }

        public Cliente(int id, string nombre, string email, string contraseña, string direccion, string telefono)
            : base(id, nombre, email, contraseña)
        {
            this.direccion = direccion;
            this.telefono = telefono;
        }

        public override string ToString()
        {
            return $"{base.ToString()} (Dirección: {direccion}, Teléfono: {telefono})";
        }
    }

}
