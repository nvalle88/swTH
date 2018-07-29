using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class EvaluacionInducion
    {
        public EvaluacionInducion()
        {
            Pregunta = new HashSet<Pregunta>();
            RealizaExamenInduccion = new HashSet<RealizaExamenInduccion>();
        }

        public int IdEvaluacionInduccion { get; set; }
        public int? MinimoAprobar { get; set; }
        public int? MaximoPuntos { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Pregunta> Pregunta { get; set; }
        public virtual ICollection<RealizaExamenInduccion> RealizaExamenInduccion { get; set; }
    }
}
