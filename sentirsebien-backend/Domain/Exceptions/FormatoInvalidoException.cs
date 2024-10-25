using System;

namespace sentirsebien_backend.Domain.Exceptions
{
    public class FormatoInvalidoException : Exception
    {
        public FormatoInvalidoException() : base("El formato proporcionado es inválido.")
        {
        }

        public FormatoInvalidoException(string message) : base(message)
        {
        }

        public FormatoInvalidoException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

