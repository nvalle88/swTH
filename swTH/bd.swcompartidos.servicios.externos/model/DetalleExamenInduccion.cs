using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class DetalleExamenInduccion
    {
        public int IdDetalleExamenInduccion { get; set; }
        public int IdRealizaExamenInduccion { get; set; }
        public int? IdPregunta { get; set; }
        public int IdRespuesta { get; set; }

        public virtual Pregunta IdPreguntaNavigation { get; set; }
        public virtual RealizaExamenInduccion IdRealizaExamenInduccionNavigation { get; set; }
        public virtual Respuesta IdRespuestaNavigation { get; set; }
    }
}
