namespace sentirsebien_backend.Domain.Exceptions
{
    public class UsuarioNoEncontradoException : Exception
    {
        public UsuarioNoEncontradoException()
            : base("El usuario no ha sido encontrado.") { }

        public UsuarioNoEncontradoException(string mensaje)
            : base(mensaje) { }

        public UsuarioNoEncontradoException(string mensaje, Exception innerException)
            : base(mensaje, innerException) { }
    }
}