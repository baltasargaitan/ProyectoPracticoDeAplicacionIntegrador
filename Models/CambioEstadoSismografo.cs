using System;

namespace RedSismica.Models
{
    public class CambioEstadoSismografo
    {
        public int Id { get; set; }

        public int SismografoId { get; set; }
        public Sismografo Sismografo { get; set; }

        public int EstadoId { get; set; }
        public Estado Estado { get; set; }

        public DateTime FechaHoraCambio { get; set; }

        public int EmpleadoId { get; set; }
        public Empleado Responsable { get; set; }
    }
}