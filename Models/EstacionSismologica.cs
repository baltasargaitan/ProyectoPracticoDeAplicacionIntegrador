namespace RedSismica.Models
{
    public class EstacionSismologica
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Ubicacion { get; set; }

        public Sismografo Sismografo { get; set; }
    }
}