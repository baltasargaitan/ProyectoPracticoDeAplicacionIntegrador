using System;

namespace RedSismica.Models
{
    public class CambioEstado
    {
        public int Id { get; set; }
        public DateTime fechaHoraInicio { get; set; }
        public DateTime? fechaHoraFin { get; set; }

        // Relaciones
        public int EstadoId { get; set; }
        public Estado estadoActual { get; set; }

        public int? MotivoFueraServicioId { get; set; }
        public MotivoFueraServicio motivoFueraServicio { get; set; }

        public int EmpleadoId { get; set; }
        public Empleado empleado { get; set; }

        public int OrdenDeInspeccionId { get; set; }
        public OrdenDeInspeccion OrdenDeInspeccion { get; set; }

        // Constructor sin parámetros (requerido por EF Core)
        public CambioEstado() { }

        // Constructor con parámetros
        public CambioEstado(DateTime fechaHoraInicio, Estado estadoActual)
        {
            this.fechaHoraInicio = fechaHoraInicio;
            this.estadoActual = estadoActual;
        }

        // Método para establecer la fecha de fin
        public void setFechaHoraFin(DateTime fechaHoraFin)
        {
            this.fechaHoraFin = fechaHoraFin;
        }
    }
}