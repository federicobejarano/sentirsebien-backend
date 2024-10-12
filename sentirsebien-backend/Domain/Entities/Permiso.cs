namespace sentirsebien_backend.Domain.Entities
{
    using System;

    public class Permiso
    {
        private string nombre;
        private string descripcion;
        private string categoria;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public string Categoria
        {
            get { return categoria; }
            set { categoria = value; }
        }

        public Permiso(string nombre, string descripcion, string categoria)
        {
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.categoria = categoria;
        }

        public override string ToString()
        {
            return $"{Nombre} ({Categoria}): {Descripcion}";
        }
    }
}
