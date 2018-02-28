using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class EscalaGrados
    {
        public EscalaGrados()
        {
            IndiceOcupacional = new HashSet<IndiceOcupacional>();
        }

        public int IdEscalaGrados { get; set; }
        public int? IdGrupoOcupacional { get; set; }
        public int? Grado { get; set; }
        public decimal Remuneracion { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<IndiceOcupacional> IndiceOcupacional { get; set; }
        public virtual GrupoOcupacional IdGrupoOcupacionalNavigation { get; set; }
    }
}
