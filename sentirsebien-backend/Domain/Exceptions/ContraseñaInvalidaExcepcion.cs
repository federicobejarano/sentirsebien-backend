namespace sentirsebien_backend.Domain.Exceptions
{
    public class ContraseñaInvalidaException : Exception
    {
        public ContraseñaInvalidaException()
            : base("La contraseña proporcionada es incorrecta.") { }

        public ContraseñaInvalidaException(string mensaje)
            : base(mensaje) { }

        public ContraseñaInvalidaException(string mensaje, Exception innerException)
            : base(mensaje, innerException) { }
    }
}