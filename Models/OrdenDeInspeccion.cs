using System;
using System.Collections.Generic;

namespace RedSismica.Models
{
    public class OrdenDeInspeccion
    {
        public int Id { get; set; }
        public string nroOrden { get; set; }
        public DateTime fechaHoraInicio { get; set; }
        public DateTime fechaHoraFinalizacion { get; set; }
        public DateTime? fechaHoraCierre { get; set; }
        public string ObservacionCierre { get; set; }

        // Relaciones
        public int EstadoId { get; set; }
        public Estado Estado { get; set; }

        public int EstacionSismologicaId { get; set; }
        public EstacionSismologica Estacion { get; set; }

        public int EmpleadoId { get; set; }
        public Empleado Empleado { get; set; }

        // Relación con CambioEstado
        public List<CambioEstado> CambiosEstado { get; set; } = new List<CambioEstado>();

        // Métodos
        public bool esDeEmpleado(int empleadoId)
        {
            return this.EmpleadoId == empleadoId;
        }

        public bool estaCompletamenteRealizado()
        {
            return Estado?.esCompletamenteRealizada() ?? false;
        }

        public string mostrarDatosOrden()
        {
            return $"Orden N° {nroOrden}\nEstación: {Estacion?.getNombre()}\nInicio: {fechaHoraInicio}\nFin: {fechaHoraFinalizacion}";
        }

        public string getNroOrden()
        {
            return nroOrden;
        }

        public DateTime getFechaFinalizacion()
        {
            return fechaHoraFinalizacion;
        }

        public void setFechaHoraCierre(DateTime fechaCierre)
        {
            fechaHoraCierre = fechaCierre;
        }

        public void setEstado(Estado nuevoEstado)
        {
            Estado = nuevoEstado;
            EstadoId = nuevoEstado.Id;
        }

        public void agregarCambioEstado(CambioEstado cambio)
        {
            CambiosEstado.Add(cambio);
        }

        public List<MotivoFueraServicio> obtenerMotivosFueraDeServicio()
        {
            // Filtra los cambios de estado que tienen motivos de fuera de servicio
            var motivos = new List<MotivoFueraServicio>();
            foreach (var cambio in CambiosEstado)
            {
                if (cambio.motivoFueraServicio != null)
                {
                    motivos.Add(cambio.motivoFueraServicio);
                }
            }
            return motivos;
        }
    }
}