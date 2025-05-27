
using System.Collections.Generic;

namespace RedSismica.Models
{
    public class Empleado
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Rol { get; set; } // Responsable de Inspecciones, etc.

        public List<OrdenInspeccion> OrdenesInspeccion { get; set; }
    }
}