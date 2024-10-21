using sentirsebien_backend.DataAccess.Models;
using sentirsebien_backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace sentirsebien_backend.DataAccess.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<sentirsebien_backend.DataAccess.Models.Usuario> Usuarios { get; set; }
        public DbSet<sentirsebien_backend.DataAccess.Models.Cliente> Clientes { get; set; }
        public DbSet<sentirsebien_backend.DataAccess.Models.Personal> Personales { get; set; }
        public DbSet<sentirsebien_backend.DataAccess.Models.Rol> Roles { get; set; }
        public DbSet<sentirsebien_backend.DataAccess.Models.Permiso> Permisos { get; set; }
        public DbSet<sentirsebien_backend.DataAccess.Models.Administrativo> Administrativos { get; set; }
        public DbSet<sentirsebien_backend.DataAccess.Models.Terapeuta> Terapeutas { get; set; }
        public DbSet<UsuarioRol> UsuarioRoles { get; set; }
        public DbSet<RolPermiso> RolPermisos { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de Usuario

            modelBuilder.Entity<sentirsebien_backend.DataAccess.Models.Usuario>()
                .HasKey(u => u.ID);

            // Configuración de Cliente

            modelBuilder.Entity<sentirsebien_backend.DataAccess.Models.Cliente>()
                .HasOne<sentirsebien_backend.DataAccess.Models.Usuario>() // relación 1:1 Cliente -> Usuario
                .WithOne()  // sin referencia inversa (Usuario -> Cliente)
                .HasForeignKey<sentirsebien_backend.DataAccess.Models.Cliente>(c => c.ID_Usuario) // clave foránea
                .IsRequired();

            // Configuración de Personal

            modelBuilder.Entity<sentirsebien_backend.DataAccess.Models.Personal>()
                .HasKey(p => p.ID);

            modelBuilder.Entity<sentirsebien_backend.DataAccess.Models.Personal>()
                .HasOne<sentirsebien_backend.DataAccess.Models.Usuario>() // relación 1:1 Personal -> Usuario
                .WithOne()  // sin referencia inversa (Usuario -> Personal)
                .HasForeignKey<sentirsebien_backend.DataAccess.Models.Personal>(c => c.ID_Usuario)  // clave foránea de usuario
                .IsRequired(); // clave foránea es necesaria

            // relación N:M Usuario - Rol

            modelBuilder.Entity<sentirsebien_backend.DataAccess.Models.Usuario>() // relación entre Usuario y UsuarioRol
                .HasMany<UsuarioRol>()
                .WithOne(ur => ur.Usuario)
                .HasForeignKey(ur => ur.ID_Usuario) // FK en UsuarioRol
                .IsRequired();

            modelBuilder.Entity<sentirsebien_backend.DataAccess.Models.Rol>() // relación entre Rol y UsuarioRol
                .HasMany<UsuarioRol>()
                .WithOne(ur => ur.Rol)
                .HasForeignKey(ur => ur.ID_Rol) // FK en UsuarioRol
                .IsRequired();

            // asegurar que solo los Clientes puedan tener el rol 'Cliente'

            modelBuilder.Entity<sentirsebien_backend.DataAccess.Models.Usuario>()
                .HasCheckConstraint("CK_Usuario_Rol_Cliente",
                    "EsCliente = 1 AND IdRol = (SELECT Id FROM Rol WHERE Codigo = 'Cliente') OR EsCliente = 0");

            // Configuración de Administrativo

            modelBuilder.Entity<sentirsebien_backend.DataAccess.Models.Administrativo>()
                .HasKey(a => a.ID);

            modelBuilder.Entity<sentirsebien_backend.DataAccess.Models.Administrativo>() // relación 1:1 Administrativo -> Personal
               .HasOne<sentirsebien_backend.DataAccess.Models.Personal>()
               .WithOne()  // sin referencia inversa (Personal -> Administrativo)
               .HasForeignKey<sentirsebien_backend.DataAccess.Models.Administrativo>(a => a.ID_Personal)  // clave foránea de personal
               .IsRequired(); // clave foránea es necesaria

            // Configuración de Terapeuta

            modelBuilder.Entity<sentirsebien_backend.DataAccess.Models.Terapeuta>()
                .HasKey(t => t.ID);

            modelBuilder.Entity<sentirsebien_backend.DataAccess.Models.Terapeuta>() // relación 1:1 Terapeuta -> Personal
                .HasOne<sentirsebien_backend.DataAccess.Models.Personal>()
                .WithOne()  // sin referencia inversa (Personal -> Terapeuta)
                .HasForeignKey<sentirsebien_backend.DataAccess.Models.Terapeuta>(t => t.ID_Personal)  // clave foránea de cliente
                .IsRequired(); // clave foránea es necesaria


            modelBuilder.Entity<sentirsebien_backend.DataAccess.Models.Terapeuta>()
                .HasOne<sentirsebien_backend.DataAccess.Models.Especialidad>() // relación unidireccional N:1 Terapeuta -> Especialidad
                .WithMany()  // sin referencia inversa (Especialidad -> Terapeuta)
                .HasForeignKey(t => t.ID_Especialidad)  // Clave foránea en Terapeuta
                .IsRequired();  // La especialidad es obligatoria para cada terapeuta

            // Restricciones de salario en Personal
            modelBuilder.Entity<sentirsebien_backend.DataAccess.Models.Personal>()
                .HasCheckConstraint("CK_Personal_Salario", "SalarioTotal > 0");

            // Configuración de Rol

            modelBuilder.Entity<sentirsebien_backend.DataAccess.Models.Rol>()
                .HasKey(r => r.ID);

            // Configuración de Permiso

            modelBuilder.Entity<sentirsebien_backend.DataAccess.Models.Permiso>()
                .HasKey(p => p.IdPermiso);

            // relación N:M Rol - Permiso

            modelBuilder.Entity<sentirsebien_backend.DataAccess.Models.Permiso>() // relación entre Permiso y RolPermiso
                .HasMany<RolPermiso>()
                .WithOne(ur => ur.Permiso)
                .HasForeignKey(ur => ur.ID_Permiso) // FK en RolPermiso
                .IsRequired();

            modelBuilder.Entity<sentirsebien_backend.DataAccess.Models.Rol>() // relación entre Rol y RolPermiso
                .HasMany<RolPermiso>()
                .WithOne(ur => ur.Rol)
                .HasForeignKey(ur => ur.ID_Rol) // FK en RolPermiso
                .IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("YourConnectionStringHere");
        }
    }
}
