using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace sentirsebien_backend.API.Dtos
{
    public record CreateUsuarioDto
    {
        public string Nombre { get; init; }
        public string Apellido { get; init; }
        public string Email { get; init; }
        public string Telefono { get; init; }
        public string Direccion { get; init; }
        public bool EsCliente { get; init; }

        public CreateUsuarioDto(string nombre, string apellido, string email, string telefono, string direccion, bool esCliente)
        {
            if (!ValidarEmail(email))
            {
                throw new ArgumentException("Email no es válido.");
            }

            if (!ValidarTelefono(telefono))
            {
                throw new ArgumentException("Número de teléfono no es válido.");
            }

            Nombre = nombre;
            Apellido = apellido;
            Email = email;
            Telefono = telefono;
            Direccion = direccion;
            EsCliente = esCliente;
        }

        public static bool ValidarEmail(string email)
        {
            string patronEmail = @"^[^@\s]+@[^@\s]+\.[^@\s]+$"; // Patrón básico para validar un correo
            return Regex.IsMatch(email, patronEmail);
        }

        public static bool ValidarTelefono(string telefono)
        {
            // Patrón para números en Argentina: +54 9 11 1234 5678 o 011 1234 5678
            string patronTelefono = @"^\+?54\s?9?\s?\d{2,4}\s?\d{4}\s?\d{4}$";
            return Regex.IsMatch(telefono, patronTelefono);
        }
    }
}

