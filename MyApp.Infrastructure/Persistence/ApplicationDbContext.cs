using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // llavess primarias Tabla Relacioin ClaseEstudianteEntity
            modelBuilder.Entity<ClaseEstudianteEntity>()
                .HasKey(x => new { x.ClaseId, x.EstudianteId });

            // Relación Clase → ClaseEstudianteEntity
            modelBuilder.Entity<ClaseEstudianteEntity>()
                .HasOne(ce => ce.Clases)
                .WithMany(c => c.Estudiantes)
                .HasForeignKey(ce => ce.ClaseId);

            // Relación Estudiante → ClaseEstudianteEntity
            modelBuilder.Entity<ClaseEstudianteEntity>()
                .HasOne(ce => ce.Estudiantes)
                .WithMany(e => e.Clases)
                .HasForeignKey(ce => ce.EstudianteId);

            ////////////////////////////////////////////////////

            // llavess primarias Tabla Relacioin Rolpermiso
            modelBuilder.Entity<RolPermisoEntity>()
                .HasKey(x => new { x.RolId, x.PermisoId });

            // Relación Rol → RolPermisoEntity
            modelBuilder.Entity<RolPermisoEntity>()
                .HasOne(ce => ce.Rol)
                .WithMany(c => c.Permisos)
                .HasForeignKey(ce => ce.RolId);

            // Relación Permiso → RolPermisoEntity
            modelBuilder.Entity<RolPermisoEntity>()
                .HasOne(ce => ce.Permisos)
                .WithMany(c => c.Roles)
                .HasForeignKey(ce => ce.PermisoId);

            //hacer unicode el dato del avatar usuario
            modelBuilder.Entity<UserEntity>()
                .Property(u => u.Avatar)
                .IsUnicode(false);
        }

        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<OfficeEntity> Offices { get; set; }
        public DbSet<EstudianteEntity> Estudiantes { get; set; }
        public DbSet<ClaseEntity> Clases { get; set; }
        public DbSet<ClaseEstudianteEntity> ClaseEstudiantes { get; set; }


        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RolEntity> Roles { get; set; }
        public DbSet<PermisosEntity> Permisos { get; set; }
        public DbSet<RolPermisoEntity> RolPermisos { get; set; }

        public DbSet<BackupEntity> Backups { get; set; }

    }
}
