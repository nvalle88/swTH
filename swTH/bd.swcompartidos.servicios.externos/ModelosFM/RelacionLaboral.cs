using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class RelacionLaboral
    {
        public RelacionLaboral()
        {
            TipoNombramiento = new HashSet<TipoNombramiento>();
        }

        public int IdRelacionLaboral { get; set; }
        public int? IdRegimenLaboral { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<TipoNombramiento> TipoNombramiento { get; set; }
        public virtual RegimenLaboral IdRegimenLaboralNavigation { get; set; }
    }
}
