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
        private readonly IGestorRolesService _gestorRolesService;

        public RegistroUsuarioService(
            IUsuarioRepository usuarioRepository,
            IPasswordService passwordService,
            IRolRepository rolRepository,
            IGestorRolesService gestorRolesService)
        {
            _usuarioRepository = usuarioRepository;
            _passwordService = passwordService;
            _rolRepository = rolRepository;
            _gestorRolesService = gestorRolesService;
        }

        // método principal: manejar pasos del proceso de registro
        public async Task<Result> RegistrarUsuario(RegistroUsuarioDTO dto)
        {
            // 1. validar si el usuario ya existe
            if (ValidarUsuarioExistente(dto.Email)) { return Result.Failure("El usuario ya está registrado."); }

            // 2. crear nueva instancia de Usuario
            var nuevoUsuario = CrearUsuario(dto);

            // 3. asignar rol por defecto ("Cliente")
            nuevoUsuario = await _gestorRolesService.AsignarRolPorDefecto(nuevoUsuario);

            // 4. persistir el nuevo usuario en la base de datos
            await _usuarioRepository.AgregarAsync(nuevoUsuario);

            return Result.Success();
        }
        
        public bool ValidarUsuarioExistente(string email)
        {
            var usuarioExistente = _usuarioRepository.ValidarEmail(email);
            return usuarioExistente == null;
        }

        public sentirsebien_backend.Domain.Entities.Usuario CrearUsuario(RegistroUsuarioDTO dto)
        {
            // crear una nueva instancia de Usuario con el DTO proporcionado
            return new sentirsebien_backend.Domain.Entities.Usuario(
                dto.Nombre,
                dto.Apellido,
                dto.Email,
                HashearContraseña(dto.Contraseña)
            );
        }

        public string HashearContraseña(string contraseña)
        {
            return _passwordService.HashPassword(contraseña);
        }
    }
}
