using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class PartidasFase
    {
        public PartidasFase()
        {
            CandidatoConcurso = new HashSet<CandidatoConcurso>();
        }

        public int IdPartidasFase { get; set; }
        public int IdIndiceOcupacionalModalidadPartida { get; set; }
        public int? IdTipoConcurso { get; set; }
        public DateTime? Fecha { get; set; }

        public virtual ICollection<CandidatoConcurso> CandidatoConcurso { get; set; }
        public virtual IndiceOcupacionalModalidadPartida IdIndiceOcupacionalModalidadPartidaNavigation { get; set; }
        public virtual TipoConcurso IdTipoConcursoNavigation { get; set; }
    }
}
