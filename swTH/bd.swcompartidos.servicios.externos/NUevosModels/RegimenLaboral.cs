using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class RegimenLaboral
    {
        public RegimenLaboral()
        {
            RelacionLaboral = new HashSet<RelacionLaboral>();
        }

        public int IdRegimenLaboral { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<RelacionLaboral> RelacionLaboral { get; set; }
    }
}
