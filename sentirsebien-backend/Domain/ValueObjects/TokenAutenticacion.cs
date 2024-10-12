using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace sentirsebien_backend.Domain.ValueObjects
{
   

    public class TokenAutenticacion
    {
        public string Valor { get; private set; }
        public DateTime Expiracion { get; private set; }

        private readonly string claveSecreta;

        public TokenAutenticacion(string claveSecreta)
        {
            this.claveSecreta = claveSecreta;
        }

        // generar un JWT
        public void GenerarToken(string usuarioId, string rol, int minutosDeExpiracion = 30)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var clave = Encoding.ASCII.GetBytes(claveSecreta);

            // información que estará dentro del token (claims)
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, usuarioId),
            new Claim(ClaimTypes.Role, rol)
        };

            // descripción del token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(minutosDeExpiracion),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(clave),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            Valor = tokenHandler.WriteToken(token);
            Expiracion = tokenDescriptor.Expires.Value;
        }

        // validar un token
        public bool ValidarToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var clave = Encoding.ASCII.GetBytes(claveSecreta);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(clave),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // permitir algunos segundos de tolerancia para expiraciones cercanas
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken securityToken);

                var jwtToken = (JwtSecurityToken)securityToken;
                return true; // token válido
            }
            catch
            {
                return false; // token no válido
            }
        }

        // comprobar si el token ha expirado
        public bool EstaExpirado()
        {
            return DateTime.UtcNow > Expiracion;
        }
    }

}
