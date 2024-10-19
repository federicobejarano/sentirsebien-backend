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
        public DbSet<UsuarioRol> UsuarioRoles { get; set; }
        public DbSet<RolPermiso> RolPermisos { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // configurar claves compuestas para tablas intermedias
            modelBuilder.Entity<UsuarioRol>().HasKey(ur => new { ur.ID_Usuario, ur.ID_Rol });
            modelBuilder.Entity<RolPermiso>().HasKey(rp => rp.IdRolPermiso);

            modelBuilder.Entity<sentirsebien_backend.DataAccess.Models.Permiso>()
                .HasKey(p => p.IdPermiso);

            // relación N:M entre Usuario y Rol
            modelBuilder.Entity<UsuarioRol>()
                .HasOne(ur => ur.Usuario)
                .WithMany(u => u.Roles)
                .HasForeignKey(ur => ur.ID_Usuario);

            modelBuilder.Entity<UsuarioRol>()
                .HasOne(ur => ur.Rol)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(ur => ur.ID_Rol);

            // relación N:M entre Rol y Permiso
            modelBuilder.Entity<RolPermiso>()
                .HasOne(rp => rp.Rol)
                .WithMany(r => r.Permisos)
                .HasForeignKey(rp => rp.ID_Rol);

            modelBuilder.Entity<RolPermiso>()
                .HasOne(rp => rp.Permiso)
                .WithMany(p => p.Roles)
                .HasForeignKey(rp => rp.ID_Permiso);
        }
    }
}
