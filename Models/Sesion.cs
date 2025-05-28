using System;

namespace RedSismica.Models
{
    public class Sesion
    {
        public int Id { get; set; }

        public DateTime fechaHoraDesde { get; set; }
        public DateTime? fechaHoraHasta { get; set; }

        // Relación 1 a 1 con Usuario
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        // Métodos

        public Usuario obtenerUsuario()
        {
            return Usuario;
        }

        public bool esVigente()
        {
            return fechaHoraHasta == null || fechaHoraHasta > DateTime.Now;
        }

        public Empleado buscarEmpleado()
        {
            return Usuario?.Empleado;
        }
    }
}
