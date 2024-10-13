using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using sentirsebien_backend.Domain.ValueObjects;

namespace sentirsebien_backend.API.Middleware
{
    public class AutenticacionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TokenAutenticacion _tokenAutenticacion;

        public AutenticacionMiddleware(RequestDelegate next, TokenAutenticacion tokenAutenticacion)
        {
            _next = next;
            _tokenAutenticacion = tokenAutenticacion;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // obtener token de autorización del encabezado de la solicitud
            if (context.Request.Headers.TryGetValue("Authorization", out StringValues authHeader))
            {
                var token = authHeader.FirstOrDefault()?.Split(" ").Last();

                // verificar token
                if (!string.IsNullOrEmpty(token) && _tokenAutenticacion.ValidarToken(token))
                {
                    // extraer claims del token y añadirlos al contexto
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var jwtToken = tokenHandler.ReadJwtToken(token);

                    if (jwtToken != null)
                    {
                        // extraer ID y rol del usuario del token
                        var userId = jwtToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value;
                        var userRole = jwtToken.Claims.First(claim => claim.Type == "role").Value;

                        // añadir información del usuario al contexto
                        context.Items["UserId"] = userId;
                        context.Items["UserRole"] = userRole;
                    }
                }
                else
                {
                    // si el token no es válido o está vacío, responder con 401 (No autorizado)
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Token inválido o ausente");
                    return;
                }
            }
            else
            {
                // si no hay token en la solicitud, responder con 401 (No autorizado)
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Token no proporcionado");
                return;
            }

            // pasar al siguiente middleware o controlador si la autenticación es válida
            await _next(context);
        }
    }
}

