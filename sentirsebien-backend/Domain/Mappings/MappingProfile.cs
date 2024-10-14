﻿using AutoMapper;
using sentirsebien_backend.Domain.Entities;
using sentirsebien_backend.DataAccess.Models;
using System.Linq;
using global::sentirsebien_backend.Domain.Services;

namespace sentirsebien_backend.Domain.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // mapping para Usuario
            CreateMap<sentirsebien_backend.Domain.Entities.Usuario, sentirsebien_backend.DataAccess.Models.Usuario>() // 
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.Id))  // Id -> ID
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.NombreCompleto))  // NombreCompleto -> Nombre
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))  // Email -> Email
                                                                                      // mapear otros campos que no están en la entidad, ejemplo: Telefono, Direccion.
                .ForMember(dest => dest.Telefono, opt => opt.Ignore()) // ignorar, no presente en la entidad
                .ForMember(dest => dest.Direccion, opt => opt.Ignore()) // ignorar, no presente en la entidad
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles.Select(rol => new UsuarioRol
                {
                    ID_Rol = (int)rol, // mapea de TipoRol al modelo
                                       // otras propiedades de UsuarioRol pueden ser mapeadas acá si es necesario
                })));

            CreateMap<sentirsebien_backend.DataAccess.Models.Usuario, sentirsebien_backend.Domain.Entities.Usuario>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID))  // ID -> Id
                .ForMember(dest => dest.NombreCompleto, opt => opt.MapFrom(src => $"{src.Nombre}"))  // Nombre -> NombreCompleto
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))  // Email -> Email
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles.Select(ur => (TipoRol)ur.ID_Rol))) // mapear desde UsuarioRol a HashSet<TipoRol>
                .ForMember(dest => dest.Contraseña, opt => opt.Ignore()); // ignorar hashContraseña (no es parte del modelo de acceso a datos)

            // Mapping para Rol
            CreateMap<sentirsebien_backend.Domain.Entities.Rol, sentirsebien_backend.DataAccess.Models.Rol>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.Id))  // Id -> ID
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.NombreRol))  // NombreRol -> Nombre
                .ForMember(dest => dest.Codigo, opt => opt.Ignore())  // ignorar, no presente en la entidad
                .ForMember(dest => dest.Area, opt => opt.Ignore())    // ignorar, no presente en la entidad
                .ForMember(dest => dest.SalarioRol, opt => opt.Ignore());  // ignorar, no presente en la entidad

            CreateMap<sentirsebien_backend.DataAccess.Models.Rol, sentirsebien_backend.Domain.Entities.Rol>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID))  // ID -> Id
                .ForMember(dest => dest.NombreRol, opt => opt.MapFrom(src => src.Nombre))  // Nombre -> NombreRol
                .ForMember(dest => dest.Tipo, opt => opt.Ignore()); // Tipo no tiene un campo equivalente directo, necesita lógica adicional.
        }
    }
}
