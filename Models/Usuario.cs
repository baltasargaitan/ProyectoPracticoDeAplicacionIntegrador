namespace RedSismica.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        public string nombreUsuario { get; set; }
        public string contraseña { get; set; }

        // Relación 1 a 1 con Empleado
        public int EmpleadoId { get; set; }
        public Empleado Empleado { get; set; }

        // Métodos
        public Empleado getRILogueado()
        {
            return Empleado;
        }
    }
}
