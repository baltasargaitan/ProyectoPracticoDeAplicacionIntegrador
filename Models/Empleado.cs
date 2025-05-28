using System.Collections.Generic;

namespace RedSismica.Models
{
    public class Empleado
    {
        public int Id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string mail { get; set; }
        public string telefono { get; set; }

        public ICollection<OrdenDeInspeccion> OrdenesInspeccion { get; set; }
        public int RolId { get; set; }
        public Rol rol { get; set; } // Relación 1 a 1 con Rol

        // Métodos
        public bool esResponsableDeReparacion()
        {
            return rol?.nombre == "Responsable de Reparación";
        }

        public string obtenerMail()
        {
            return mail;
        }

        public Empleado getEmpleado()
        {
            return this;
        }
    }
}
