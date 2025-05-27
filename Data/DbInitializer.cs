using Microsoft.EntityFrameworkCore;
using RedSismica.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RedSismica.Data
{
    public static class DbInitializer
    {
        public static void Initialize(RedSismicaContext context)
        {
            context.Database.Migrate();

            // Verifica si ya hay datos
            if (context.Empleados.Any()) return;

            // Empleados
            var empleados = new List<Empleado>
            {
                new Empleado { Id = 1, Nombre = "Juan Pérez", Rol = "Responsable de Inspecciones" },
                new Empleado { Id = 2, Nombre = "Laura Díaz", Rol = "Responsable de Reparaciones" },
                new Empleado { Id = 3, Nombre = "Carlos Ruiz", Rol = "Responsable de Reparaciones" }
            };
            context.Empleados.AddRange(empleados);

            // Tipos de motivo de baja
            var tiposMotivo = new List<TipoMotivoBaja>
            {
                new TipoMotivoBaja { Id = 1, Descripcion = "Falla eléctrica" },
                new TipoMotivoBaja { Id = 2,Nombre = "Falla" , Descripcion = "Falla en sensor" },
                new TipoMotivoBaja { Id = 3,Nombre = "Vandalismo" , Descripcion = "Vandalismo" },
                new TipoMotivoBaja { Id = 4,Nombre = "Clima" ,Descripcion = "Condiciones climáticas" }
            };
            context.TiposMotivoBaja.AddRange(tiposMotivo);

            // Estaciones y sismógrafos
            var estaciones = new List<EstacionSismologica>
            {
                new EstacionSismologica
                {
                    Id = 1,
                    Nombre = "Estación Norte",
                    Ubicacion = "Córdoba",
                    Sismografo = new Sismografo
                    {
                        Id = 1,
                        Identificador = "SG-1001",
                        Estado = "Operativo"
                    }
                },
                new EstacionSismologica
                {
                    Id = 2,
                    Nombre = "Estación Sur",
                    Ubicacion = "Neuquén",
                    Sismografo = new Sismografo
                    {
                        Id = 2,
                        Identificador = "SG-1002",
                        Estado = "Operativo"
                    }
                }
            };
            context.Estaciones.AddRange(estaciones);

            // Órdenes de inspección
            var ordenes = new List<OrdenInspeccion>
            {
                new OrdenInspeccion
                {
                    Id = 1,
                    EmpleadoId = 1, // Juan Pérez
                    EstacionSismologicaId = 1,
                    EstaCerrada = false,
                    ObservacionCierre = null
                },
                new OrdenInspeccion
                {
                    Id = 2,
                    EmpleadoId = 1,
                    EstacionSismologicaId = 2,
                    EstaCerrada = false,
                    ObservacionCierre = null
                }
            };
            context.OrdenesInspeccion.AddRange(ordenes);

            context.SaveChanges();
        }
    }
}