using System.Collections.Generic;

namespace RedSismica.Models
{
    public class Sismografo
    {
        public int Id { get; set; }
        public string Identificador { get; set; }

        public int EstadoId { get; set; }
        public Estado Estado { get; set; }

        public int EstacionSismologicaId { get; set; }
        public EstacionSismologica EstacionSismologica { get; set; }

        public List<CambioEstadoSismografo> CambiosEstado { get; set; }
    }
}