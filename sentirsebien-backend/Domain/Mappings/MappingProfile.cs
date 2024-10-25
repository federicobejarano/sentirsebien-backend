using AutoMapper;
using sentirsebien_backend.Domain.Entities;
using sentirsebien_backend.DataAccess.Models;

namespace sentirsebien_backend.Domain.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // mapear Usuario entidad de dominio a modelo de datos
            CreateMap<sentirsebien_backend.Domain.Entities.Usuario, DataAccess.Models.Usuario>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.Apellido, opt => opt.MapFrom(src => src.Apellido))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Telefono, opt => opt.Ignore()) // ignorar si no es necesario
                .ForMember(dest => dest.Direccion, opt => opt.Ignore())
                .ForMember(dest => dest.EsCliente, opt => opt.Ignore())
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles.Select(rol => new UsuarioRol
                {
                    ID_Rol = rol.Id,
                    ID_Usuario = src.Id
                })));

            // mapear Usuario modelo de datos a entidad de dominio
            CreateMap<DataAccess.Models.Usuario, sentirsebien_backend.Domain.Entities.Usuario>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.Apellido, opt => opt.MapFrom(src => src.Apellido))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles.Select(ur => new DataAccess.Models.Rol
                {
                    ID = ur.ID_Rol
                })))
                .ForMember(dest => dest.Contraseña, opt => opt.Ignore()); // contraseña manejada con seguridad

            // mapear Rol entidad de dominio a modelo de datos
            CreateMap<sentirsebien_backend.Domain.Entities.Rol, DataAccess.Models.Rol>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.NombreRol))
                .ForMember(dest => dest.Codigo, opt => opt.Ignore()) // asignar si es necesario
                .ForMember(dest => dest.Area, opt => opt.Ignore())
                .ForMember(dest => dest.SalarioRol, opt => opt.Ignore())
                .ForMember(dest => dest.Permisos, opt => opt.Ignore()) // mapear según la lógica
                .ForMember(dest => dest.Usuarios, opt => opt.Ignore()); // evitar bucles de mapeo circular

            // mapear Rol modelo de datos a entidad de dominio
            CreateMap<DataAccess.Models.Rol, sentirsebien_backend.Domain.Entities.Rol>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.NombreRol, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.Tipo, opt => opt.Ignore()); // asignar tipo específico si aplica

            // mapear Permiso entidad de dominio a modelo de datos
            CreateMap<sentirsebien_backend.Domain.Entities.Permiso, DataAccess.Models.Permiso>()
                .ForMember(dest => dest.IdPermiso, opt => opt.Ignore()) // ID se genera automáticamente
                .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.NombrePermiso))
                .ForMember(dest => dest.TipoPermiso, opt => opt.MapFrom(src => src.Categoria))
                .ForMember(dest => dest.AccionPermiso, opt => opt.MapFrom(src => src.Descripcion))
                .ForMember(dest => dest.Roles, opt => opt.Ignore());

            // mapear Permiso modelo de datos a entidad de dominio
            CreateMap<DataAccess.Models.Permiso, sentirsebien_backend.Domain.Entities.Permiso>()
                .ForMember(dest => dest.NombrePermiso, opt => opt.MapFrom(src => src.Codigo))
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.AccionPermiso))
                .ForMember(dest => dest.Categoria, opt => opt.MapFrom(src => src.TipoPermiso));

            // mapear RolPermiso ??
        }
    }
}
