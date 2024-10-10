namespace sentirsebien_backend.Dtos
{
    public class Personal
    {
        public int ID { get; set; }
        public int IDUsuario { get; set; } // FK de Usuario
        public decimal SalarioTotal { get; set; } // Calculado a partir de otros atributos

        // Relación 1:1 con Usuario
        public Usuario Usuario { get; set; }

        // Relación N:1 con Rol (mediante PersonalRol)
        public List<PersonalRol> Roles { get; set; }

        // Relación 1:1 con Administrativo o Terapeuta
        public Administrativo Administrativo { get; set; }
        public Terapeuta Terapeuta { get; set; }

        public Personal()
        {
            Roles = new List<PersonalRol>();
        }
    }

    public class Rol
    {
        public int ID { get; set; }
        public string Codigo { get; set; } // Abreviación área-nombre
        public string Nombre { get; set; }
        public string Area { get; set; }
        public decimal SalarioRol { get; set; }

        // Relación N:1 con Personal (mediante PersonalRol)
        public List<PersonalRol> PersonalRoles { get; set; }

        public Rol()
        {
            PersonalRoles = new List<PersonalRol>();
        }
    }

    public class PersonalRol
    {
        public int IDPersonal { get; set; } // FK de Personal
        public int IDRol { get; set; } // FK de Rol
        public DateTime FechaInicio { get; set; }
        public int HorasSemana { get; set; }

        // Relación N:1 con Personal
        public Personal Personal { get; set; }

        // Relación N:1 con Rol
        public Rol Rol { get; set; }
    }

    public class Administrativo
    {
        public int ID { get; set; }
        public int IDPersonal { get; set; } // FK de Personal

        // Relación 1:1 con Personal
        public Personal Personal { get; set; }
    }

    public class Terapeuta
    {
        public int ID { get; set; }
        public int IDPersonal { get; set; } // FK de Personal
        public int IDEspecialidad { get; set; } // FK de Especialidad

        // Relación 1:1 con Personal
        public Personal Personal { get; set; }

        // Relación N:1 con Especialidad (esto se manejará en la tabla de Especialidad)
        public Especialidad Especialidad { get; set; }
    }

    public class Especialidad
    {
        public int ID { get; set; }
        public string Nombre { get; set; }

        // Relación 1:N con Terapeuta
        public List<Terapeuta> Terapeutas { get; set; }

        public Especialidad()
        {
            Terapeutas = new List<Terapeuta>();
        }
    }
}
