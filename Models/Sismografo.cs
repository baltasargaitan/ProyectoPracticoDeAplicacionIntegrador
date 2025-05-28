using System;
using System.Collections.Generic;

namespace RedSismica.Models
{
    public class Sismografo
    {
        public int Id { get; set; }
        public string IdentificacionSismografo { get; set; } // Identificador único
        public string nroSerie { get; set; } // Número de serie
        public DateTime fechaAdquisicion { get; set; } // Fecha de adquisición

        // Relación 1 a 1 con EstacionSismologica
        public int EstacionSismologicaId { get; set; }
        public EstacionSismologica estacion { get; set; }

        // Relación 1 a 1 con Estado actual
        public int EstadoId { get; set; }
        public Estado Estado { get; set; }

        // Relación 1 a muchos con CambioEstado
        public List<CambioEstado> CambiosEstado { get; set; } = new List<CambioEstado>();

        // Métodos

        public bool sosDeEstacionSismologica(int estacionId)
        {
            return EstacionSismologicaId == estacionId;
        }

        public string getIdentificadorSismografo()
        {
            return IdentificacionSismografo;
        }

        public static Sismografo nuevo(string identificador, string numeroSerie, DateTime fecha, EstacionSismologica estacionInicial)
        {
            return new Sismografo
            {
                IdentificacionSismografo = identificador,
                nroSerie = numeroSerie,
                fechaAdquisicion = fecha,
                estacion = estacionInicial,
                EstacionSismologicaId = estacionInicial.Id
            };
        }

        public Estado obtenerEstadoActual()
        {
            return Estado;
        }

        public void setEstadoActual(Estado nuevoEstado)
        {
            Estado = nuevoEstado;
            EstadoId = nuevoEstado.Id;
        }

        public void crearCambioEstado(Estado nuevoEstado, DateTime fechaCambio)
        {
            var cambio = new CambioEstado(fechaCambio, nuevoEstado)
            {
                EstadoId = nuevoEstado.Id,
                fechaHoraInicio = fechaCambio,
            };
            CambiosEstado.Add(cambio);
            setEstadoActual(nuevoEstado);
        }

        public void enviarAReparar()
        {
            // Lógica de ejemplo: cambiar estado a "En Reparación"
            var estadoReparacion = new Estado("Sismográfico", "En Reparación");
            crearCambioEstado(estadoReparacion, DateTime.Now);
        }
    }
}
