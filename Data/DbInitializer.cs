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

            // Estaciones y sismógrafos
            var estaciones = new List<EstacionSismologica>
            {
                new EstacionSismologica
                {
                    Id = 1,
                    codigoEstacion = "EST-001",
                    nombre = "Estación Norte",
                    latitud = -31.4167,
                    longitud = -64.1833,
                    estadoActual = estados.First(e => e.nombreEstado == "Operativo"),
                    Sismografos = new List<Sismografo>
                    {
                        new Sismografo { Id = 1, IdentificacionSismografo = "SG-1001", nroSerie = "SN-001", fechaAdquisicion = DateTime.Now.AddYears(-2), Estado = estados.First(e => e.nombreEstado == "Operativo") }
                    }
                },
                
                new EstacionSismologica
                {
                    Id = 2,
                    codigoEstacion = "EST-002",
                    nombre = "Estación Sur",
                    latitud = -38.9516,
                    longitud = -68.0591,
                    estadoActual = estados.First(e => e.nombreEstado == "Operativo"),
                    Sismografos = new List<Sismografo>
                    {
                        new Sismografo { Id = 2, IdentificacionSismografo = "SG-1002", nroSerie = "SN-002", fechaAdquisicion = DateTime.Now.AddYears(-3), Estado = estados.First(e => e.nombreEstado == "Operativo") }
                    }
                }
            };
            context.Estaciones.AddRange(estaciones);
            context.SaveChanges();
            
            // Órdenes de inspección
            var ordenes = new List<OrdenDeInspeccion>();
            for (int i = 1; i <= 50; i++) // Crear 50 órdenes de inspección
            {
                ordenes.Add(new OrdenDeInspeccion
                {
                    Id = i,
                    nroOrden = $"OI-{i:D3}",
                    fechaHoraInicio = DateTime.Now.AddDays(-i),
                    fechaHoraFinalizacion = DateTime.Now.AddDays(-i + 1),
                    EmpleadoId = 1,
                    EstacionSismologicaId = estaciones.First(e => e.Id == (i % 2 == 0 ? 1 : 2)).Id,
                    EstadoId = estados.First(e => e.nombreEstado == "Completamente Realizada").Id
                });
            };


            //Motivos 
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
            context.OrdenesInspeccion.AddRange(ordenes);
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