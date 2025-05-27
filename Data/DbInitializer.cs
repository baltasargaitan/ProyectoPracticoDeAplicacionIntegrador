using RedSismica.Models;
using System.Linq;

namespace RedSismica.Data
{
    public static class DbInitializer
    {
        public static void Initialize(RedSismicaContext context)
        {
            if (context.OrdenesInspeccion.Any())
                return; // Ya hay datos

            // Sismógrafos
            var sismografo1 = new Sismografo { Estado = "Activo", Identificador = "SISMO-001" };
            var sismografo2 = new Sismografo { Estado = "Activo", Identificador = "SISMO-002" };
            var sismografo3 = new Sismografo { Estado = "Activo", Identificador = "SISMO-003" };

            // Estaciones
            var estacion1 = new EstacionSismologica { Nombre = "Estación Central", Sismografo = sismografo1 };
            var estacion2 = new EstacionSismologica { Nombre = "Estación Norte", Sismografo = sismografo2 };
            var estacion3 = new EstacionSismologica { Nombre = "Estación Sur", Sismografo = sismografo3 };

            // Empleados
            var empleado1 = new Empleado { Nombre = "Ana Torres" };
            var empleado2 = new Empleado { Nombre = "Luis Gómez" };
            var empleado3 = new Empleado { Nombre = "María Pérez" };

            // Órdenes de inspección
            var orden1 = new OrdenInspeccion
            {
                EstaCerrada = false,
                EstacionSismologica = estacion1,
                Responsable = empleado1
            };
            var orden2 = new OrdenInspeccion
            {
                EstaCerrada = false,
                EstacionSismologica = estacion2,
                Responsable = empleado2
            };
            var orden3 = new OrdenInspeccion
            {
                EstaCerrada = false,
                EstacionSismologica = estacion3,
                Responsable = empleado3
            };

            context.Sismografos.AddRange(sismografo1, sismografo2, sismografo3);
            context.Estaciones.AddRange(estacion1, estacion2, estacion3);
            context.Empleados.AddRange(empleado1, empleado2, empleado3);
            context.OrdenesInspeccion.AddRange(orden1, orden2, orden3);

            context.SaveChanges();
        }
    }
}