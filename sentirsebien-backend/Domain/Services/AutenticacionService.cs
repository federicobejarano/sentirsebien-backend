namespace sentirsebien_backend.Domain.Services
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using Microsoft.IdentityModel.Tokens;

    public class AutenticacionService : IAutenticacionService
    {
        private readonly string claveSecreta;

        public AutenticacionService(string claveSecreta)
        {
            this.claveSecreta = claveSecreta;
        }

        // autenticar al usuario mediante username y password
        public bool AutenticarUsuario(string username, string password)
        {
            // acá iría la lógica para validar las credenciales del usuario.
            // -> integrar un servicio de base de datos o cualquier otro método de validación
            // a continuaión un ejemplo simplificado donde el usuario y contraseña son validados:
            if (username == "usuarioEjemplo" && password == "contraseñaEjemplo")
            {
                return true;
            }
            return false;
        }

        // generar el token JWT después de autenticar al usuario
        public string GenerarToken(string username)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var clave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(claveSecreta));
            var credenciales = new SigningCredentials(clave, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: "tuSistema",
                audience: "tusUsuarios",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30), // Expiración del token
                signingCredentials: credenciales
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        // invalidar el token (puedes implementar lógica de almacenamiento si es necesario)
        public void InvalidarToken(string token)
        {
            // en una implementación avanzada se pueden guardar los tokens invalidados
            // en una base de datos o lista en memoria y verificar que no estén en esa lista.
            //
            // el siguiente método es solo ilustrativo:
            Console.WriteLine("Token invalidado: " + token);
        }
    }

}
