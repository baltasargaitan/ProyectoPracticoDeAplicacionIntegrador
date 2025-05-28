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

            // Estados
            var estados = new List<Estado>
            {
                new Estado("Orden de Inspección", "Operativo"),
                new Estado("Orden de Inspección", "Fuera de Servicio"),
                new Estado("Orden de Inspección", "Cerrada"),
                new Estado("Orden de Inspección", "Completamente Realizada"),
                new Estado("Sismográfico", "Operativo"),
                new Estado("Sismográfico", "Fuera de Servicio"),
                new Estado("Sismográfico", "En Reparación")
            };
            context.Estados.AddRange(estados);
            context.SaveChanges();

            // Roles
            var roles = new List<Rol>
            {
                new Rol { Id = 1, nombre = "Responsable de Inspecciones", descripcion = "Encargado de realizar inspecciones" },
                new Rol { Id = 2, nombre = "Responsable de Reparaciones", descripcion = "Encargado de realizar reparaciones" }
            };
            context.Roles.AddRange(roles);
            context.SaveChanges();

            // Empleados
            var empleados = new List<Empleado>
            {
                new Empleado { Id = 1, nombre = "Juan", apellido = "Pérez", mail = "juan.perez@example.com", telefono = "123456789", RolId = 1 },
                new Empleado { Id = 2, nombre = "Laura", apellido = "Díaz", mail = "laura.diaz@example.com", telefono = "987654321", RolId = 2 },
                new Empleado { Id = 3, nombre = "Carlos", apellido = "Ruiz", mail = "carlos.ruiz@example.com", telefono = "456789123", RolId = 2 }
            };
            context.Empleados.AddRange(empleados);
            context.SaveChanges();

            // Tipos de motivo de baja
            var tiposMotivo = new List<MotivoTipo>
            {
                new MotivoTipo { Id = 1, tipoMotivo = "Falla eléctrica", Descripcion = "Problemas eléctricos en el equipo" },
                new MotivoTipo { Id = 2, tipoMotivo = "Falla en sensor", Descripcion = "El sensor dejó de funcionar" },
                new MotivoTipo { Id = 3, tipoMotivo = "Vandalismo", Descripcion = "Daños causados por terceros" },
                new MotivoTipo { Id = 4, tipoMotivo = "Condiciones climáticas", Descripcion = "Daños por tormentas o clima extremo" }
            };
            context.MotivosTipo.AddRange(tiposMotivo);
            context.SaveChanges();

            // Estaciones y sismógrafos (relación 1 a 1)
            var estaciones = new List<EstacionSismologica>();
            for (int i = 1; i <= 2; i++) // Crear 2 estaciones
            {
                estaciones.Add(new EstacionSismologica
                {
                    Id = i,
                    codigoEstacion = $"EST-{i:D3}",
                    nombre = i == 1 ? "Estación Norte" : "Estación Sur",
                    latitud = i == 1 ? -31.4167 : -38.9516,
                    longitud = i == 1 ? -64.1833 : -68.0591,
                    estadoActual = estados.First(e => e.nombreEstado == "Operativo"),
                    Sismografo = new Sismografo
                    {
                        Id = i,
                        IdentificacionSismografo = $"SG-{i:D4}", // Genera identificador único dinámico
                        nroSerie = $"SN-{i:D3}",
                        fechaAdquisicion = DateTime.Now.AddYears(-i),
                        Estado = estados.First(e => e.nombreEstado == "Operativo")
                    }
                });
            }
            context.Estaciones.AddRange(estaciones);
            context.SaveChanges();




            //ordenes 



            var ordenes = new List<OrdenDeInspeccion>();
            int sismografoCount = estaciones.Count; // Número total de sismógrafos (uno por estación)

            for (int i = 1; i <= 2; i++) // Crear 50 órdenes de inspección
            {
                var estacion = estaciones[(i - 1) % sismografoCount]; // Alterna entre estaciones
                var sismografoId = estacion.Sismografo.Id; // Obtiene el ID del sismógrafo asociado a la estación

                ordenes.Add(new OrdenDeInspeccion
                {
                    Id = i,
                    nroOrden = $"OI-{i:D3}",
                    fechaHoraInicio = DateTime.Now.AddDays(-i),
                    fechaHoraFinalizacion = DateTime.Now.AddDays(-i + 1),
                    EmpleadoId = 1,
                    EstacionSismologicaId = estacion.Id, // Asocia la estación
                    EstadoId = estados.First(e => e.nombreEstado == "Completamente Realizada").Id
                });
            }
            context.OrdenesInspeccion.AddRange(ordenes);
            context.SaveChanges();;

            // Motivos fuera de servicio
            var motivosFueraServicio = new List<MotivoFueraServicio>
            {
                new MotivoFueraServicio("Problemas eléctricos detectados")
                {
                    MotivoTipoId = tiposMotivo.First(m => m.tipoMotivo == "Falla eléctrica").Id
                },
                new MotivoFueraServicio("Sensor dañado por vandalismo")
                {
                    MotivoTipoId = tiposMotivo.First(m => m.tipoMotivo == "Vandalismo").Id
                },
                new MotivoFueraServicio("Daños causados por tormenta")
                {
                    MotivoTipoId = tiposMotivo.First(m => m.tipoMotivo == "Condiciones climáticas").Id
                }
            };
            context.MotivosFueraServicio.AddRange(motivosFueraServicio);
            context.SaveChanges();

            // Usuario y sesión inicial
            var usuario = new Usuario
            {
                nombreUsuario = "admin",
                contraseña = "admin123",
                EmpleadoId = 1
            };
            context.Usuarios.Add(usuario);
            context.SaveChanges();

            var sesion = new Sesion
            {
                fechaHoraDesde = DateTime.Now,
                UsuarioId = usuario.Id
            };
            context.Sesiones.Add(sesion);
            context.SaveChanges();
        }
    }
}