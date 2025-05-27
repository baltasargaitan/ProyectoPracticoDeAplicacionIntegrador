using System;
using System.Collections.Generic;
using System.Linq;
using RedSismica.Models;
using RedSismica.Data;
using Microsoft.EntityFrameworkCore;

namespace RedSismica.Models
{
    public class GestorCierreInspeccion
    {
        private readonly RedSismicaContext _context;

        public GestorCierreInspeccion(RedSismicaContext context)
        {
            _context = context;
        }

        public List<OrdenInspeccion> getOrdenesCompletamenteRealizadas(int empleadoId)
        {
            return _context.OrdenesInspeccion
                .Include(o => o.EstacionSismologica)
                    .ThenInclude(e => e.Sismografo)
                        .ThenInclude(s => s.Estado)
                .Include(o => o.Empleado)
                .Where(o => !o.EstaCerrada && o.EmpleadoId == empleadoId)
                .ToList();
        }

        public OrdenInspeccion tomarSeleccionOrden(int ordenId)
        {
            return _context.OrdenesInspeccion
                .Include(o => o.EstacionSismologica)
                    .ThenInclude(e => e.Sismografo)
                        .ThenInclude(s => s.Estado)
                .Include(o => o.MotivosBaja)
                .FirstOrDefault(o => o.Id == ordenId);
        }

        public void tomarObservacionCierre(OrdenInspeccion orden, string observacion)
        {
            orden.ObservacionCierre = observacion;
        }

        public void tomarMotivosFueraDeServicio(OrdenInspeccion orden, int[] motivoIds, string[] comentarios)
        {
            var motivos = motivoIds.Select((mId, i) => new MotivoBajaSismografo
            {
                TipoMotivoBajaId = mId,
                Comentario = comentarios[i],
                OrdenInspeccionId = orden.Id
            }).ToList();

            orden.MotivosBaja = motivos;
        }

        public void cerrarOrden(OrdenInspeccion orden, int empleadoId)
        {
            orden.EstaCerrada = true;
            orden.FechaCierre = DateTime.Now;

            var sismografo = orden.EstacionSismologica.Sismografo;

            // Buscar el estado "Fuera de Servicio"
            var estadoFueraServicio = _context.Estados.FirstOrDefault(e => e.Nombre == "Fuera de Servicio");
            if (estadoFueraServicio == null)
                throw new Exception("No existe el estado 'Fuera de Servicio' en la base de datos.");

            sismografo.EstadoId = estadoFueraServicio.Id;
            sismografo.Estado = estadoFueraServicio;

            var cambioEstado = new CambioEstadoSismografo
            {
                SismografoId = sismografo.Id,
                EstadoId = estadoFueraServicio.Id,
                Estado = estadoFueraServicio,
                FechaHoraCambio = DateTime.Now,
                EmpleadoId = empleadoId
            };

            _context.CambiosEstadoSismografo.Add(cambioEstado);
            _context.SaveChanges();

            notificarCambioEstado(sismografo, cambioEstado, orden.MotivosBaja);
        }

        public void notificarCambioEstado(Sismografo sismografo, CambioEstadoSismografo cambio, List<MotivoBajaSismografo> motivos)
        {
            // Simulación de notificación
            Console.WriteLine($"[NOTIFICACIÓN] Sismógrafo {sismografo.Identificador} marcado como '{cambio.Estado.Nombre}' el {cambio.FechaHoraCambio}.");
            foreach (var motivo in motivos)
            {
                Console.WriteLine($"Motivo: {motivo.TipoMotivoBajaId} - {motivo.Comentario}");
            }
        }
    }
}