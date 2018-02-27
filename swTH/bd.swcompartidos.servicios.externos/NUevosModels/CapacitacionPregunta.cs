using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class CapacitacionPregunta
    {
        public int IdCapacitacionPregunta { get; set; }
        public int IdCapacitacionTipoPregunta { get; set; }
        public int IdCapacitacionRespuesta { get; set; }
        public int? IdCapacitacionEncuesta { get; set; }
        public string Descripcion { get; set; }

        public virtual CapacitacionEncuesta IdCapacitacionEncuestaNavigation { get; set; }
        public virtual CapacitacionRespuesta IdCapacitacionRespuestaNavigation { get; set; }
        public virtual CapacitacionTipoPregunta IdCapacitacionTipoPreguntaNavigation { get; set; }
    }
}
