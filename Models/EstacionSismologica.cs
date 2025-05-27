namespace RedSismica.Models
{
    public class EstacionSismologica
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public int SismografoId { get; set; }
        public Sismografo Sismografo { get; set; }
    }
}