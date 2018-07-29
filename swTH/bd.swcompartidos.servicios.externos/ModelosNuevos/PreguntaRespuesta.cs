using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class PreguntaRespuesta
    {
        public int IdPreguntaRespuesta { get; set; }
        public bool? Verdadero { get; set; }
        public int IdPregunta { get; set; }
        public int IdRespuesta { get; set; }

        public virtual Pregunta IdPreguntaNavigation { get; set; }
        public virtual Respuesta IdRespuestaNavigation { get; set; }
    }
}
