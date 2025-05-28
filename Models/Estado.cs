using System;

namespace RedSismica.Models
{
    public class Estado
    {
        public int Id { get; set; }
        public string ambito { get; set; } // Ámbito del estado
        public string nombreEstado { get; set; } // Nombre del estado

        // Métodos
        public Estado(string ambito, string nombreEstado)
        {
            this.ambito = ambito;
            this.nombreEstado = nombreEstado;
        }

        public bool esCompletamenteRealizada()
        {
            return nombreEstado == "Completamente Realizada";
        }
        public bool esCerrada()
        {
            return nombreEstado == "Cerrada";
        }

        public bool esAmbitoOrdenInspeccion()
        {
            return this.ambito == "Orden de Inspección";
        }


        public bool esAmbitoSismografico()
        {
            return this.ambito == "Sismográfico";
        }

    }
}