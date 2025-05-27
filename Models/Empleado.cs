using System.Collections.Generic;

namespace RedSismica.Models
{
    public class Empleado
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int RolId { get; set; }
        public Rol Rol { get; set; }

        public List<OrdenInspeccion> OrdenesInspeccion { get; set; }
    }
}