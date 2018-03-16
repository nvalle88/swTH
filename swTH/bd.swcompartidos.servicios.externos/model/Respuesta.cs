using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class Respuesta
    {
        public Respuesta()
        {
            DetalleExamenInduccion = new HashSet<DetalleExamenInduccion>();
            PreguntaRespuesta = new HashSet<PreguntaRespuesta>();
        }

        public int IdRespuesta { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<DetalleExamenInduccion> DetalleExamenInduccion { get; set; }
        public virtual ICollection<PreguntaRespuesta> PreguntaRespuesta { get; set; }
    }
}
