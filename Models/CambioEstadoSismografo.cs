using System;

namespace RedSismica.Models
{
    public class CambioEstadoSismografo
    {
        public int Id { get; set; }
        public string Estado { get; set; }
        public DateTime FechaHora { get; set; }

        public int SismografoId { get; set; }
        public Sismografo Sismografo { get; set; }

        public int ResponsableId { get; set; }
        public Empleado Responsable { get; set; }
    }
}