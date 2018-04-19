using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace bd.swth.entidades.Negocio
{
    public partial class Calificacion
    {
        [Key]
        public int IdCalificacion { get; set; }
        public int IdTipoCalificacion { get; set; }
        public int IdCandidatoConcurso { get; set; }
        public int? Puntaje { get; set; }

        public virtual CandidatoConcurso CandidatoConcurso { get; set; }
        public virtual TipoCalificacion TipoCalificacion{ get; set; }

    }
}
