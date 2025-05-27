using System;

namespace RedSismica.Models
{
    public class Sesion
    {
        public int Id { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public DateTime? FechaHoraFin { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public bool EstaActiva()
        {
            return FechaHoraFin == null;
        }
    }
}