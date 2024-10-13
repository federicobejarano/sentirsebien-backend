using sentirsebien_backend.Domain.Services;

namespace sentirsebien_backend.Domain.Entities
{
    public class Rol
    {
        private Guid id;
        private string nombreRol;
        private TipoRol tipo;

        public Guid Id
        {
            get { return id; }
            set { id = value; }
        }

        public string NombreRol
        {
            get { return nombreRol; }
            set { nombreRol = value; }
        }

        public TipoRol Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        public Rol(Guid id, string nombreRol, TipoRol tipo)
        {
            this.id = id;
            this.nombreRol = nombreRol;
            this.tipo = tipo;
        }

        public override string ToString()
        {
            return $"Rol: {nombreRol}, Tipo: {tipo}";
        }
    }
}
