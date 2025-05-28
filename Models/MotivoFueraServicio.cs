namespace RedSismica.Models
{
    public class MotivoFueraServicio
    {
        public int Id { get; set; }
        public string comentario { get; set; } // Comentario del motivo

        public int MotivoTipoId { get; set; }
        public MotivoTipo motivoTipo { get; set; } // Relación con MotivoTipo

        // Constructor sin parámetros (requerido por EF Core)
        public MotivoFueraServicio() { }

        // Constructor con parámetros
        public MotivoFueraServicio(string comentario)
        {
            this.comentario = comentario;
        }

        // Método para establecer el comentario
        public void setComentario(string comentario)
        {
            this.comentario = comentario;
        }
    }
}