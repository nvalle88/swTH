using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class Calificacion
    {
        public int IdCalificacion { get; set; }
        public int IdTipoCalificacion { get; set; }
        public int IdCandidatoConcurso { get; set; }
        public int? Puntaje { get; set; }

        public virtual CandidatoConcurso IdCandidatoConcursoNavigation { get; set; }
        public virtual TipoCalificacion IdTipoCalificacionNavigation { get; set; }
    }
}
