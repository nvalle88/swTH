using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class PreguntaEvaluacionEvento
    {
        public PreguntaEvaluacionEvento()
        {
            DetalleEvaluacionEvento = new HashSet<DetalleEvaluacionEvento>();
        }

        public int IdPreguntaEvaluacionEvento { get; set; }
        public string Descripcion { get; set; }
        public bool? Facilitador { get; set; }
        public bool? Organizador { get; set; }
        public bool? ConocimientoObtenidos { get; set; }

        public virtual ICollection<DetalleEvaluacionEvento> DetalleEvaluacionEvento { get; set; }
    }
}
