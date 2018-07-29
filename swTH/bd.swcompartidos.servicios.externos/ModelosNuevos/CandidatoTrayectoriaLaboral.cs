using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class CandidatoTrayectoriaLaboral
    {
        public int IdCandidatoTrayectoriaLaboral { get; set; }
        public int IdCandidato { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Institucion { get; set; }

        public virtual Candidato IdCandidatoNavigation { get; set; }
    }
}
