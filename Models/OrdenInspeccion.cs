using System;
using System.Collections.Generic;

namespace RedSismica.Models
{
    public class OrdenInspeccion
    {
        public int Id { get; set; }
        public string ObservacionCierre { get; set; }
        public bool EstaCerrada { get; set; }
        public DateTime? FechaCierre { get; set; }

        public int EstacionSismologicaId { get; set; }
        public EstacionSismologica EstacionSismologica { get; set; }

        public int EmpleadoId { get; set; }
        public Empleado Empleado { get; set; }

        public List<MotivoBajaSismografo> MotivosBaja { get; set; }
    }
}