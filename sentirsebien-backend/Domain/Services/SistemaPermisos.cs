using sentirsebien_backend.Domain.Entities;

namespace sentirsebien_backend.Domain.Services
{
    public class SistemaPermisos
    {
        // Diccionario de permisos por rol
        public static Dictionary<TipoRol, List<Permiso>> PermisosPorRol = new Dictionary<TipoRol, List<Permiso>>
        {
            { TipoRol.Administrador, new List<Permiso>
                {
                    Permisos.VerTurnos,
                    Permisos.CrearTurnos,
                    Permisos.ModificarTurnos,
                    Permisos.CancelarTurnos,
                    Permisos.VerServicios,
                    Permisos.AgregarServicios,
                    Permisos.ModificarServicios,
                    Permisos.EliminarServicios,
                    Permisos.VerUsuarios,
                    Permisos.CrearUsuarios,
                    Permisos.ModificarUsuarios,
                    Permisos.EliminarUsuarios,
                    Permisos.AsignarServiciosAlPersonal,
                    Permisos.ModificarHorariosPersonal,
                    Permisos.GestionarRolesPersonal,
                    Permisos.VerReportesFinancieros,
                    Permisos.GenerarReportesFinancieros,
                    Permisos.GestionarFacturas,
                    Permisos.ConfigurarSistema,
                    Permisos.GestionarPermisos
                }
            },
            { TipoRol.Recepcionista, new List<Permiso>
                {
                    Permisos.VerTurnos,
                    Permisos.CrearTurnos,
                    Permisos.ModificarTurnos,
                    Permisos.CancelarTurnos,
                    Permisos.VerServicios,
                    Permisos.VerUsuarios,
                    Permisos.CrearUsuarios,
                    Permisos.AsignarServiciosAlPersonal
                }
            },
            { TipoRol.Gerente, new List<Permiso>
                {
                    Permisos.VerTurnos,
                    Permisos.ModificarTurnos,
                    Permisos.CancelarTurnos,
                    Permisos.VerServicios,
                    Permisos.AgregarServicios,
                    Permisos.ModificarServicios,
                    Permisos.VerUsuarios,
                    Permisos.ModificarUsuarios,
                    Permisos.VerReportesFinancieros,
                    Permisos.GenerarReportesFinancieros
                }
            },
            { TipoRol.Especialista, new List<Permiso>
                {
                    Permisos.VerTurnos,
                    Permisos.ModificarTurnos,
                    Permisos.CancelarTurnos,
                    Permisos.ModificarHorariosPersonal
                }
            },
            { TipoRol.Cliente, new List<Permiso>
                {
                    Permisos.VerTurnos,
                    Permisos.ModificarTurnos,
                    Permisos.CancelarTurnos
                }
            }
        };
    };
}
