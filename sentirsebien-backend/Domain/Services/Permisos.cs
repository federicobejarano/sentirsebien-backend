using sentirsebien_backend.Domain.Entities;

namespace sentirsebien_backend.Domain.Services
{
    public static class Permisos
    {
        // Permisos de Turnos
        public static Permiso VerTurnos = new Permiso("Ver Turnos", "Permite visualizar los turnos agendados", "Turnos");
        public static Permiso CrearTurnos = new Permiso("Crear Turnos", "Permite agendar nuevos turnos", "Turnos");
        public static Permiso ModificarTurnos = new Permiso("Modificar Turnos", "Permite editar turnos existentes", "Turnos");
        public static Permiso CancelarTurnos = new Permiso("Cancelar Turnos", "Permite cancelar turnos", "Turnos");

        // Permisos de Servicios
        public static Permiso VerServicios = new Permiso("Ver Servicios", "Permite visualizar los servicios del spa", "Servicios");
        public static Permiso AgregarServicios = new Permiso("Agregar Servicios", "Permite agregar nuevos servicios", "Servicios");
        public static Permiso ModificarServicios = new Permiso("Modificar Servicios", "Permite modificar servicios existentes", "Servicios");
        public static Permiso EliminarServicios = new Permiso("Eliminar Servicios", "Permite eliminar servicios del catálogo", "Servicios");

        // Permisos de Usuarios
        public static Permiso VerUsuarios = new Permiso("Ver Usuarios", "Permite ver la lista de usuarios registrados", "Usuarios");
        public static Permiso CrearUsuarios = new Permiso("Crear Usuarios", "Permite registrar nuevos usuarios o empleados", "Usuarios");
        public static Permiso ModificarUsuarios = new Permiso("Modificar Usuarios", "Permite modificar la información de los usuarios", "Usuarios");
        public static Permiso EliminarUsuarios = new Permiso("Eliminar Usuarios", "Permite eliminar usuarios del sistema", "Usuarios");

        // Permisos de Personal
        public static Permiso AsignarServiciosAlPersonal = new Permiso("Asignar Servicios", "Permite asignar servicios a empleados", "Personal");
        public static Permiso ModificarHorariosPersonal = new Permiso("Modificar Horarios", "Permite modificar los horarios del personal", "Personal");
        public static Permiso GestionarRolesPersonal = new Permiso("Gestionar Roles", "Permite gestionar los roles del personal", "Personal");

        // Permisos Financieros
        public static Permiso VerReportesFinancieros = new Permiso("Ver Reportes Financieros", "Permite visualizar reportes financieros", "Finanzas");
        public static Permiso GenerarReportesFinancieros = new Permiso("Generar Reportes Financieros", "Permite generar reportes financieros", "Finanzas");
        public static Permiso GestionarFacturas = new Permiso("Gestionar Facturas", "Permite crear y modificar facturas", "Finanzas");

        // Permisos de Administración
        public static Permiso ConfigurarSistema = new Permiso("Configurar Sistema", "Permite modificar configuraciones del sistema", "Administración");
        public static Permiso GestionarPermisos = new Permiso("Gestionar Permisos", "Permite gestionar los permisos de los usuarios", "Administración");
    }
}
