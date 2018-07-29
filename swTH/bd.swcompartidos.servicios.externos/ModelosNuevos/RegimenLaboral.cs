using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class RegimenLaboral
    {
        public RegimenLaboral()
        {
            RelacionLaboral = new HashSet<RelacionLaboral>();
            VacacionRelacionLaboral = new HashSet<VacacionRelacionLaboral>();
        }

        public int IdRegimenLaboral { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<RelacionLaboral> RelacionLaboral { get; set; }
        public virtual ICollection<VacacionRelacionLaboral> VacacionRelacionLaboral { get; set; }
    }
}
