namespace sentirsebien_backend.Domain.Entities
{
    public class Rol
    {
        private int id;
        private string nombreRol;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string NombreRol
        {
            get { return nombreRol; }
            set { nombreRol = value; }
        }

        public Rol(int id, string nombreRol)
        {
            this.id = id;
            this.nombreRol = nombreRol;
        }

        public override string ToString()
        {
            return $"Rol: {nombreRol}";
        }
    }

}
