using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class Candidato
    {
        public Candidato()
        {
            CandidatoConcurso = new HashSet<CandidatoConcurso>();
            CandidatoEstudio = new HashSet<CandidatoEstudio>();
            CandidatoTrayectoriaLaboral = new HashSet<CandidatoTrayectoriaLaboral>();
        }

        public int IdCandidato { get; set; }
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public virtual ICollection<CandidatoConcurso> CandidatoConcurso { get; set; }
        public virtual ICollection<CandidatoEstudio> CandidatoEstudio { get; set; }
        public virtual ICollection<CandidatoTrayectoriaLaboral> CandidatoTrayectoriaLaboral { get; set; }
    }
}
