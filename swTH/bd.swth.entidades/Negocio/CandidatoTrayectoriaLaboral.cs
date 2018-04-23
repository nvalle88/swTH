using System;
using System.Collections.Generic;
using System.Text;

namespace bd.swth.entidades.Negocio
{
    public class CandidatoTrayectoriaLaboral
    {
        public int IdCandidatoTrayectoriaLaboral { get; set; }
        public int IdCandidato { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Institucion { get; set; }

        public virtual Candidato Candidato { get; set; }
    }
}
