using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using sentirsebien_backend.Domain.ValueObjects;
using sentirsebien_backend.Domain.Services;

namespace sentirsebien_backend.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly string _secretKey;
        private readonly int _expirationMinutes;

        public TokenService(IConfiguration configuration)
        {
            _secretKey = configuration["JwtSettings:SecretKey"];
            _expirationMinutes = int.Parse(configuration["JwtSettings:ExpirationMinutes"]);
        }

        public async Task<TokenAutenticacion> GenerarTokenAsync(DatosDeAutenticacionUsuario datosDeAutenticacion, DatosDeAutorizacionUsuario datosDeAutorizacion)
        {
            // crear los claims basados en la información del usuario
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, datosDeAutenticacion.UserId.ToString()),
                new Claim(ClaimTypes.Email, datosDeAutenticacion.Email),
            };

            // agregar roles a los claims
            var roles = datosDeAutorizacion.Roles.Select(r => r.NombreRol).ToList();
            foreach (var rol in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, rol));
            }

            // agregar permisos a los claims
            var permisos = datosDeAutorizacion.Permisos.Select(p => p.NombrePermiso).ToList();
            foreach (var permiso in permisos)
            {
                claims.Add(new Claim("permiso", permiso));
            }

            // crear clave de seguridad
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // configurar fechas
            var fechaCreacion = DateTime.UtcNow;
            var fechaExpiracion = fechaCreacion.AddMinutes(_expirationMinutes);

            // crear token
            var token = new JwtSecurityToken(
                issuer: "sentirsebien_backend",
                audience: "sentirsebien_frontend",
                claims: claims,
                notBefore: fechaCreacion,
                expires: fechaExpiracion,
                signingCredentials: creds
            );

            // serializar token
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            // crear y retornar la instancia de TokenAutenticacion
            return new TokenAutenticacion(
                token: tokenString,
                fechaCreacion: fechaCreacion,
                fechaExpiracion: fechaExpiracion,
                userId: datosDeAutenticacion.UserId,
                email: datosDeAutenticacion.Email,
                roles: roles.AsReadOnly(),
                permisos: permisos.AsReadOnly()
            );
        }

        public async Task<bool> ValidarTokenAsync(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_secretKey);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = "sentirsebien_backend",
                    ValidAudience = "sentirsebien_frontend",
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> InvalidarTokenAsync(string token)
        {
            // no implementado. Agregar mecanismo de invalidación

            return await Task.FromResult(false);
        }
    }
}
