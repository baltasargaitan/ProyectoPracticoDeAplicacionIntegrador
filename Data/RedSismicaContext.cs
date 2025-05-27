using Microsoft.EntityFrameworkCore;
using RedSismica.Models; // Make sure this namespace contains MotivoBajaSismografo

namespace RedSismica.Data
{
    public class RedSismicaContext : DbContext
    {
        public RedSismicaContext(DbContextOptions<RedSismicaContext> options) : base(options) {}

        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<OrdenInspeccion> OrdenesInspeccion { get; set; }
        public DbSet<EstacionSismologica> Estaciones { get; set; }
        public DbSet<Sismografo> Sismografos { get; set; }
        public DbSet<CambioEstadoSismografo> CambiosEstado { get; set; }
        public DbSet<TipoMotivoBaja> TiposMotivoBaja { get; set; }
        public DbSet<MotivoBajaSismografo> MotivosBaja { get; set; }
    }
}