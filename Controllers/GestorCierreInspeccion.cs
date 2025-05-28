using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RedSismica.Data;
using RedSismica.Models;

namespace RedSismica.Models
{
    public class GestorCierreInspeccion
    {
        private readonly RedSismicaContext _context;
        private Sesion sesionActual;

        public GestorCierreInspeccion(RedSismicaContext context, Sesion sesion)
        {
            _context = context;
            sesionActual = sesion;
        }

        // 1. Buscar Responsable de Inspección (RI) logueado
        public Empleado buscarRILogueado()
        {
            return _context.Empleados.FirstOrDefault(e => e.Id == 1); // Hardcode para demo
        }

                // 2. Buscar órdenes completamente realizadas asignadas al RI
        public List<OrdenDeInspeccion> buscarOrdenes()
        {
            var empleado = buscarRILogueado();
            if (empleado == null)
            {
                throw new Exception("No se encontró un empleado asociado a la sesión actual.");
            }

            return _context.OrdenesInspeccion
                .Include(o => o.Estacion)
                    .ThenInclude(e => e.Sismografo) // Incluye el sismógrafo de la estación
                .Include(o => o.Empleado) // Incluye el empleado responsable
                .Where(o => o.EmpleadoId == empleado.Id && o.EstadoId == 4) // Estado "Completamente Realizada"
                .ToList();
        }

        // 3. Ordenar las órdenes
        public List<OrdenDeInspeccion> ordenarOrdenes(List<OrdenDeInspeccion> ordenes)
        {
            return ordenes.OrderBy(o => o.fechaHoraInicio).ToList();
        }

                // 4. Tomar selección de una orden
        public OrdenDeInspeccion tomarSeleccionOrden(int ordenId)
        {
            return _context.OrdenesInspeccion
                .Include(o => o.Estacion)
                    .ThenInclude(e => e.Sismografo) // Incluye el sismógrafo de la estación
                .Include(o => o.Empleado) // Incluye el empleado responsable
                .Include(o => o.CambiosEstado) // Incluye los cambios de estado
                .FirstOrDefault(o => o.Id == ordenId);
        }

        // 5. Tomar observación de cierre
        public void tomarObservacion(OrdenDeInspeccion orden, string observacion)
        {
            if (string.IsNullOrWhiteSpace(observacion))
                throw new ArgumentException("La observación no puede estar vacía.");

            orden.ObservacionCierre = observacion;
        }

        // 6. Buscar motivos fuera de servicio disponibles
        public List<MotivoTipo> buscarMotivosFueraServicio()
        {
            return _context.MotivosTipo.ToList();
        }

        // 7. Tomar motivo fuera de servicio seleccionado
        public void tomarMotivoFueraServicio(OrdenDeInspeccion orden, int motivoId, string comentario)
        {
            var motivoTipo = _context.MotivosTipo.FirstOrDefault(m => m.Id == motivoId);
            if (motivoTipo == null) throw new Exception($"MotivoTipo con Id {motivoId} no encontrado.");

            var estadoFueraServicio = _context.Estados.FirstOrDefault(e => e.nombreEstado == "Fuera de Servicio");
            if (estadoFueraServicio == null) throw new Exception("No existe un estado 'Fuera de Servicio'.");

            var motivo = new MotivoFueraServicio(comentario)
            {
                motivoTipo = motivoTipo
            };

            var cambioEstado = new CambioEstado(DateTime.Now, estadoFueraServicio)
            {
                motivoFueraServicio = motivo,
                OrdenDeInspeccionId = orden.Id,
                EmpleadoId = buscarRILogueado().Id
            };

            orden.agregarCambioEstado(cambioEstado);
            _context.CambiosEstado.Add(cambioEstado);
            _context.SaveChanges();
        }

        // 8. Validar datos mínimos requeridos para cerrar la orden
        public bool validarDatosMinimosReqParaCierre(OrdenDeInspeccion orden)
        {
            return !string.IsNullOrWhiteSpace(orden.ObservacionCierre) && orden.CambiosEstado.Any(c => c.motivoFueraServicio != null);
        }

        // 9. Obtener estado "Cerrado" para la orden
        public Estado buscarEstadoCerradoParaOrden()
        {
            return _context.Estados
                .AsEnumerable() // Forzar evaluación en memoria
                .FirstOrDefault(e => e.esAmbitoOrdenInspeccion() && e.esCerrada());
        }

        // 10. Obtener estado "Fuera de Servicio" para el sismógrafo
        public Estado buscarEstadoFueraDeServicioParaSismografo()
        {
            return _context.Estados
                .AsEnumerable() // Forzar evaluación en memoria
                .FirstOrDefault(e => e.esAmbitoSismografico() && e.nombreEstado == "Fuera de Servicio");
        }
                // 11. Cerrar la orden
        public void cerrarOrden(OrdenDeInspeccion orden)
        {
            var estadoCerrado = buscarEstadoCerradoParaOrden();
            if (estadoCerrado == null)
                throw new Exception("No existe un estado 'Cerrado' para órdenes de inspección.");

            orden.setEstado(estadoCerrado); // Cambia el estado a "Cerrada"
            orden.setFechaHoraCierre(DateTime.Now); // Establece la fecha y hora de cierre

            _context.OrdenesInspeccion.Update(orden); // Actualiza la orden en el contexto
            _context.SaveChanges(); // Guarda los cambios en la base de datos
        }

        // 12. Enviar sismógrafo para reparación
        public void enviarSismografoParaReparacion(EstacionSismologica estacion)
        {
            estacion.Sismografo.enviarAReparar();
        }

        // 13. Obtener mail del responsable de reparación
        public string obtenerMailResponsableDeReparacion()
        {
            var empleado = _context.Empleados.FirstOrDefault(e => e.esResponsableDeReparacion());
            return empleado?.obtenerMail();
        }

        // 14. Publicar en monitores
        public void publicarEnMonitores(string mensaje)
        {
            Console.WriteLine($"[MONITOR] {mensaje}");
        }

        // 15. Enviar notificaciones por mail
        public void enviarNotificacionesPorMail(string destinatario, string asunto, string cuerpo)
        {
            Console.WriteLine($"[EMAIL] Enviando a: {destinatario}\nAsunto: {asunto}\nCuerpo: {cuerpo}");
        }

        // 16. Finalizar caso de uso
        public void finCU()
        {
            Console.WriteLine("Caso de uso finalizado.");
        }
    }
}