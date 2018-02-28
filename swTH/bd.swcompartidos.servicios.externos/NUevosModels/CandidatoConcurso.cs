using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class CandidatoConcurso
    {
        public CandidatoConcurso()
        {
            Calificacion = new HashSet<Calificacion>();
        }

        public int IdCandidatoConcurso { get; set; }
        public string CodigoSocioEmpleo { get; set; }
        public bool? Preseleccionado { get; set; }
        public bool? Ganador { get; set; }
        public int IdCandidato { get; set; }
        public int IdPartidasFase { get; set; }

        public virtual ICollection<Calificacion> Calificacion { get; set; }
        public virtual Candidato IdCandidatoNavigation { get; set; }
        public virtual PartidasFase IdPartidasFaseNavigation { get; set; }
    }
}
