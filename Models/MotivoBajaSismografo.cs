namespace RedSismica.Models
{
    public class MotivoBajaSismografo
    {
        public int Id { get; set; }
        public string Comentario { get; set; }

        public int TipoMotivoBajaId { get; set; }
        public TipoMotivoBaja Tipo { get; set; }

        public int OrdenInspeccionId { get; set; }
        public OrdenInspeccion Orden { get; set; }
    }
}