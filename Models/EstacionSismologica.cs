using System;

namespace RedSismica.Models
{
    public class EstacionSismologica
    {
        public int Id { get; set; }

        public string codigoEstacion { get; set; } // Código único
        public string nombre { get; set; } // Nombre de la estación
        public DateTime fechaSolicitudCertificacion { get; set; } // Fecha de solicitud
        public string documentoCertificacionAdquirida { get; set; } // Documento PDF, por ejemplo
        public string nroCertificacionAdquisicion { get; set; } // Número de certificación
        public double latitud { get; set; } // Coordenada
        public double longitud { get; set; } // Coordenada

        public int EstadoId { get; set; }
        public Estado estadoActual { get; set; } // Estado de la estación

        // Relación 1 a 1 con Sismógrafo
        public int SismografoId { get; set; }
        public Sismografo Sismografo { get; set; } // Relación directa con un único sismógrafo

        // Métodos

        public int obtenerIdSismografo()
        {
            return Sismografo?.Id ?? 0;
        }

        public string getNombre()
        {
            return nombre;
        }

        public void ponerSismografoFueraDeServicio()
        {
            var estadoFueraServicio = new Estado("Sismográfico", "Fuera de Servicio");
            Sismografo?.crearCambioEstado(estadoFueraServicio, DateTime.Now);
        }

        public string getCodigoEstacion()
        {
            return codigoEstacion;
        }
    }
}