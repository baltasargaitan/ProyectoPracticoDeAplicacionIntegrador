using Microsoft.EntityFrameworkCore;
using RedSismica.Models;

namespace RedSismica.Data
{
    public class RedSismicaContext : DbContext
    {
        public RedSismicaContext(DbContextOptions<RedSismicaContext> options)
            : base(options)
        {
        }

        // DbSets para las entidades del dominio
        public DbSet<EstacionSismologica> Estaciones { get; set; }
        public DbSet<Sismografo> Sismografos { get; set; }
        public DbSet<OrdenDeInspeccion> OrdenesInspeccion { get; set; }
        public DbSet<MotivoTipo> MotivosTipo { get; set; }
        public DbSet<MotivoFueraServicio> MotivosFueraServicio { get; set; }
        public DbSet<CambioEstado> CambiosEstado { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Sesion> Sesiones { get; set; }
        public DbSet<Rol> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relación 1 a 1 entre EstacionSismologica y Sismografo
            modelBuilder.Entity<EstacionSismologica>()
                .HasOne(e => e.Sismografo)
                .WithOne(s => s.estacion)
                .HasForeignKey<Sismografo>(s => s.EstacionSismologicaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación uno a muchos entre Empleado y OrdenDeInspeccion
            modelBuilder.Entity<OrdenDeInspeccion>()
                .HasOne(o => o.Empleado)
                .WithMany(e => e.OrdenesInspeccion)
                .HasForeignKey(o => o.EmpleadoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación uno a muchos entre OrdenDeInspeccion y CambioEstado
            modelBuilder.Entity<CambioEstado>()
                .HasOne(c => c.OrdenDeInspeccion)
                .WithMany(o => o.CambiosEstado)
                .HasForeignKey(c => c.OrdenDeInspeccionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación uno a uno entre CambioEstado y Estado
            modelBuilder.Entity<CambioEstado>()
                .HasOne(c => c.estadoActual)
                .WithMany()
                .HasForeignKey(c => c.EstadoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación uno a uno entre CambioEstado y MotivoFueraServicio
            modelBuilder.Entity<CambioEstado>()
                .HasOne(c => c.motivoFueraServicio)
                .WithMany()
                .HasForeignKey(c => c.MotivoFueraServicioId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación uno a uno entre CambioEstado y Empleado (Responsable)
            modelBuilder.Entity<CambioEstado>()
                .HasOne(c => c.empleado)
                .WithMany()
                .HasForeignKey(c => c.EmpleadoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación uno a uno entre Sismografo y Estado
            modelBuilder.Entity<Sismografo>()
                .HasOne(s => s.Estado)
                .WithMany()
                .HasForeignKey(s => s.EstadoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación uno a uno entre Empleado y Rol
            modelBuilder.Entity<Empleado>()
                .HasOne(e => e.rol)
                .WithMany()
                .HasForeignKey(e => e.RolId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación uno a uno entre Usuario y Empleado
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Empleado)
                .WithMany()
                .HasForeignKey(u => u.EmpleadoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación uno a uno entre Sesion y Usuario
            modelBuilder.Entity<Sesion>()
                .HasOne(s => s.Usuario)
                .WithMany()
                .HasForeignKey(s => s.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}