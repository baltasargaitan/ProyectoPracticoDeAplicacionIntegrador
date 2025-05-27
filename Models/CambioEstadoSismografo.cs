using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RedSismica.Models
{
    public class CambioEstadoSismografo
    {
        public int Id { get; set; }

        public int SismografoId { get; set; }
        public Sismografo Sismografo { get; set; }

        public string Estado { get; set; } // Ej: "Fuera de Servicio"

        public DateTime FechaHoraCambio { get; set; }

        public int EmpleadoId { get; set; }  // FK al empleado responsable del cierre
        public Empleado Responsable { get; set; } // ✅ Esta propiedad soluciona el error
    }
}
