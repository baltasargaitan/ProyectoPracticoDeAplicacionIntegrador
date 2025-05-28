namespace RedSismica.Models
{
    public class MotivoTipo
    {
        public int Id { get; set; }
        public string tipoMotivo { get; set; }
        public string Descripcion { get; set; }
         public string getDescripcion()
        {
            return Descripcion;
        }
    }
}