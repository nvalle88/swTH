using System;
using System.Collections.Generic;
using System.Text;

namespace bd.swth.entidades.Negocio
{
    public class CandidatoEstudio
    {
        public int IdCandidatoEstudio { get; set; }
        public DateTime? FechaGraduado { get; set; }
        public string Observaciones { get; set; }
        public int IdTitulo { get; set; }
        public int IdCandidato { get; set; }
        public string NoSenescyt { get; set; }

        public virtual Candidato Candidato { get; set; }
        public virtual Titulo Titulo { get; set; }
    }
}
