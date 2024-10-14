namespace sentirsebien_backend.Domain.Services
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using Microsoft.IdentityModel.Tokens;
    using sentirsebien_backend.DataAccess.Repositories;

    public class AutenticacionService : IAutenticacionService
    {
        private readonly string claveSecreta;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPasswordService _passwordService;

        public AutenticacionService(string claveSecreta, IUsuarioRepository usuarioRepository, IPasswordService passwordService)
        {
            this.claveSecreta = claveSecreta;
            this._usuarioRepository = usuarioRepository;
            this._passwordService = passwordService;
        }

        public bool RegistrarUsuario(string username, string email, string password)
        {
            // verificar si el usuario o el correo electrónico ya existen en la base de datos
            if (_usuarioRepository.ObtenerPorNombreUsuario(username) != null || _usuarioRepository.ObtenerPorEmail(email) != null)
            {
                return false; // Usuario o email ya registrados
            }

            // hashear la contraseña
            string hashedPassword = _passwordService.HashPassword(password);

            // crear un conjunto de roles para el nuevo usuario (puedes modificarlo según tu lógica)
            var roles = new HashSet<TipoRol>(); 


            // crear el nuevo usuario
            var nuevoUsuario = new sentirsebien_backend.Domain.Entities.Usuario(
                id: 0, // ID se asignará en la base de datos (o puedes gestionar manualmente si lo prefieres)
                nombre: username,
                email: email,
                contraseña: password,
                passwordService: _passwordService,
                roles: roles
            );

            // guardar el usuario en la base de datos
            _usuarioRepository.Agregar(nuevoUsuario);

            return true; // Usuario registrado con éxito
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
