namespace RedSismica.Models
{
    public class Rol
    {
        public int Id { get; set; }
        public string nombre { get; set; } // Ej: "Técnico", "Responsable de Reparación"
        public string descripcion { get; set; }

        // Métodos
        public string getNombre()
        {
            return nombre;
        }
    }
}
