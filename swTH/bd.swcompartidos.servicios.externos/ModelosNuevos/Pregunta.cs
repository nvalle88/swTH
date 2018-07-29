using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class Pregunta
    {
        public Pregunta()
        {
            DetalleExamenInduccion = new HashSet<DetalleExamenInduccion>();
            PreguntaRespuesta = new HashSet<PreguntaRespuesta>();
        }

        public int IdPregunta { get; set; }
        public string Nombre { get; set; }
        public int IdEvaluacionInduccion { get; set; }

        public virtual ICollection<DetalleExamenInduccion> DetalleExamenInduccion { get; set; }
        public virtual ICollection<PreguntaRespuesta> PreguntaRespuesta { get; set; }
        public virtual EvaluacionInducion IdEvaluacionInduccionNavigation { get; set; }
    }
}
