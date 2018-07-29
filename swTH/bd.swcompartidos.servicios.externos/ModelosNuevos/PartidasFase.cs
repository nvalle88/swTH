using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class PartidasFase
    {
        public PartidasFase()
        {
            CandidatoConcurso = new HashSet<CandidatoConcurso>();
        }

        public int IdPartidasFase { get; set; }
        public int IdIndiceOcupacional { get; set; }
        public int? IdTipoConcurso { get; set; }
        public DateTime? Fecha { get; set; }
        public int? Vacantes { get; set; }
        public int? Estado { get; set; }
        public bool Contrato { get; set; }

        public virtual ICollection<CandidatoConcurso> CandidatoConcurso { get; set; }
        public virtual IndiceOcupacional IdIndiceOcupacionalNavigation { get; set; }
        public virtual TipoConcurso IdTipoConcursoNavigation { get; set; }
    }
}
