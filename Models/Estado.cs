namespace RedSismica.Models
{
    public class Estado
    {
        public int Id { get; set; }
        public string Nombre { get; set; } // Ej: "Fuera de Servicio", "Online", etc.

        public bool EsFueraDeServicio()
        {
            return Nombre == "Fuera de Servicio";
        }
    }
}