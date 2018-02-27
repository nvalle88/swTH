using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class SituacionPropuesta
    {
        public int IdSituacionPropuesta { get; set; }
        public int? IdDependencia { get; set; }
        public DateTime? Ano { get; set; }
        public int? NumeroPropuesta { get; set; }
        public int? Brecha { get; set; }
        public string Observaciones { get; set; }

        public virtual Dependencia IdDependenciaNavigation { get; set; }
    }
}
