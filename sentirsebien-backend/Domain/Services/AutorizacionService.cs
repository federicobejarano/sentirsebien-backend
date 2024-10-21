using sentirsebien_backend.DataAccess.Repositories;

namespace sentirsebien_backend.Domain.Services
{
    public class AutorizacionService // : IAutorizacionService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IRolRepository _rolRepository;
        private readonly IPermisoRepository _permisoRepository;

        public AutorizacionService(IUsuarioRepository usuarioRepository, IRolRepository rolRepository, IPermisoRepository permisoRepository)
        {
            _usuarioRepository = usuarioRepository;
            _rolRepository = rolRepository;
            _permisoRepository = permisoRepository;
        }

        // métodos

    }
}
