using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class CandidatoEstudio
    {
        public int IdCandidatoEstudio { get; set; }
        public DateTime? FechaGraduado { get; set; }
        public string Observaciones { get; set; }
        public int IdTitulo { get; set; }
        public int IdCandidato { get; set; }
        public string NoSenescyt { get; set; }

        public virtual Candidato IdCandidatoNavigation { get; set; }
        public virtual Titulo IdTituloNavigation { get; set; }
    }
}
