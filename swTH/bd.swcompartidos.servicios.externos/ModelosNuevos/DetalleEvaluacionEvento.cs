using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class DetalleEvaluacionEvento
    {
        public int IdDetalleEvaluacionEvento { get; set; }
        public int? Calificacion { get; set; }
        public int IdPreguntasEvaluacionEvento { get; set; }
        public int? IdEvaluacionEvento { get; set; }
        public bool? Conocimiento { get; set; }

        public virtual EvaluacionEvento IdEvaluacionEventoNavigation { get; set; }
        public virtual PreguntaEvaluacionEvento IdPreguntasEvaluacionEventoNavigation { get; set; }
    }
}
