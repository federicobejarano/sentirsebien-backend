using sentirsebien_backend.API.Dtos;
using sentirsebien_backend.DataAccess.Repositories;
using sentirsebien_backend.Domain.Shared;

namespace sentirsebien_backend.Domain.Services
{
    public class RegistroUsuarioService : IRegistroUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPasswordService _passwordService;
        private readonly IRolRepository _rolRepository;

        public RegistroUsuarioService(
            IUsuarioRepository usuarioRepository,
            IPasswordService passwordService,
            IRolRepository rolRepository)
        {
            _usuarioRepository = usuarioRepository;
            _passwordService = passwordService;
            _rolRepository = rolRepository;
        }

        // 1. validar si un usuario ya existe en la base de datos por su email
        public bool ValidarUsuarioExistente(string email)
        {
            var usuarioExistente = _usuarioRepository.ValidarEmail(email);
            return usuarioExistente == null;
        }

        // 2. crear una instancia de la entidad Usuario a partir del DTO
        public sentirsebien_backend.Domain.Entities.Usuario CrearUsuario(RegistroUsuarioDTO dto)
        {
            // hashear la contraseña
            var hashContraseña = _passwordService.HashPassword(dto.Contraseña);

            // crear una nueva instancia de Usuario con el DTO proporcionado
            return new sentirsebien_backend.Domain.Entities.Usuario(
                dto.Nombre,
                dto.Apellido,
                dto.Email,
                hashContraseña
            );
        }

        // 3. manejar todo el proceso de registro de usuario
        public async Task<Result> RegistrarUsuario(RegistroUsuarioDTO dto)
        {
            // validar si el usuario ya existe
            if (ValidarUsuarioExistente(dto.Email))
            {
                return Result.Failure("El usuario ya está registrado.");
            }

            // crear nueva instancia de Usuario
            var nuevoUsuario = CrearUsuario(dto);

            // obtener rol por defecto ("Cliente")
            var rolCliente = await _rolRepository.GetByNombreAsync("Cliente");

            // asignar el rol al nuevo usuario
            nuevoUsuario.AsignarRol(rolCliente);

            // persistir el nuevo usuario en la base de datos
            await _usuarioRepository.AgregarAsync(nuevoUsuario);

            return Result.Success();
        }
    }
}
