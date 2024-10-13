namespace sentirsebien_backend.API.Dtos
{
    public record class GetPersonalDto(int ID, int IDUsuario, decimal SalarioTotal)
    {
        /*
        public int ID { get; set; }
        public int IDUsuario { get; set; } // FK de Usuario
        public decimal SalarioTotal { get; set; } // Calculado a partir de otros atributos
        */

        // Relación 1:1 con Usuario
        public required GetUsuarioDto Usuario { get; set; }

        // Relación N:1 con Rol (mediante PersonalRol)
        public required List<GetPersonalRolDto> Roles { get; init; } = new List<GetPersonalRolDto>();

        // Relación 1:1 con Administrativo o Terapeuta
        public required GetAdministrativoDto Administrativo { get; set; }
        public required GetTerapeutaDto Terapeuta { get; set; }
    }

    public record class GetRolDto
    {
        public required int ID { get; set; }
        public required string Codigo { get; set; } // Abreviación área-nombre
        public required string Nombre { get; set; }
        public required string Area { get; set; }
        public required decimal SalarioRol { get; set; }

        // Relación N:1 con Personal (mediante PersonalRol)
        public required List<GetPersonalRolDto> PersonalRoles { get; set; } = new List<GetPersonalRolDto>();
    }

    public record class GetPersonalRolDto // tabla intermedia
    {
        public int IDPersonal { get; set; } // FK de Personal
        public int IDRol { get; set; } // FK de Rol
        public DateTime FechaInicio { get; set; }
        public int HorasSemana { get; set; }

        // Relación N:1 con Personal
        public required GetPersonalDto Personal { get; set; }

        // Relación N:1 con Rol
        public required GetRolDto Rol { get; set; }
    }

    public record class GetAdministrativoDto
    {
        public int ID { get; set; }
        public int IDPersonal { get; set; } // FK de Personal

        // Relación 1:1 con Personal
        public required GetPersonalDto Personal { get; set; }
    }

    public record class GetTerapeutaDto
    {
        public int ID { get; set; }
        public int IDPersonal { get; set; } // FK de Personal
        public int IDEspecialidad { get; set; } // FK de Especialidad

        // Relación 1:1 con Personal
        public required GetPersonalDto Personal { get; set; }

        // Relación N:1 con Especialidad (esto se manejará en la tabla de Especialidad)
        public required GetEspecialidadDto Especialidad { get; set; }
    }

    public record class GetEspecialidadDto
    {
        public required int ID { get; set; }
        public required string Nombre { get; set; }

        // Relación 1:N con Terapeuta
        public required List<GetTerapeutaDto> Terapeutas { get; set; } = new List<GetTerapeutaDto>();
    }
}
