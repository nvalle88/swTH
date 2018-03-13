using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class Candidato
    {
        public Candidato()
        {
            CandidatoConcurso = new HashSet<CandidatoConcurso>();
        }

        public int IdCandidato { get; set; }
        public int? IdPersona { get; set; }

        public virtual ICollection<CandidatoConcurso> CandidatoConcurso { get; set; }
        public virtual Persona IdPersonaNavigation { get; set; }
    }
}
