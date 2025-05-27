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

        public DbSet<EstacionSismologica> Estaciones { get; set; }        public DbSet<Sismografo> Sismografos { get; set; }
        public DbSet<OrdenInspeccion> OrdenesInspeccion { get; set; }
        public DbSet<TipoMotivoBaja> TiposMotivoBaja { get; set; }
        public DbSet<MotivoBajaSismografo> MotivosBajaSismografo { get; set; }
        public DbSet<CambioEstadoSismografo> CambiosEstadoSismografo { get; set; }
        public DbSet<Empleado> Empleados { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relación uno a muchos entre OrdenInspeccion y MotivoBajaSismografo
            modelBuilder.Entity<MotivoBajaSismografo>()
                .HasOne(m => m.OrdenInspeccion)
                .WithMany(o => o.MotivosBaja)
                .HasForeignKey(m => m.OrdenInspeccionId);

            // Relación uno a muchos entre EstacionSismologica y Sismografo
            modelBuilder.Entity<EstacionSismologica>()
                .HasOne(e => e.Sismografo)
                .WithOne(s => s.EstacionSismologica)
                .HasForeignKey<Sismografo>(s => s.EstacionSismologicaId);

            // Relación uno a muchos entre Empleado y OrdenInspeccion
            modelBuilder.Entity<OrdenInspeccion>()
                .HasOne(o => o.Empleado)
                .WithMany(e => e.OrdenesInspeccion)
                .HasForeignKey(o => o.EmpleadoId);

            // CambioEstadoSismografo (auditoría de cambios)
            modelBuilder.Entity<CambioEstadoSismografo>()
                .HasOne(c => c.Sismografo)
                .WithMany(s => s.CambiosEstado)
                .HasForeignKey(c => c.SismografoId);

            modelBuilder.Entity<CambioEstadoSismografo>()
                .HasOne(c => c.Responsable)
                .WithMany()
                .HasForeignKey(c => c.EmpleadoId);
        }
    }
}
