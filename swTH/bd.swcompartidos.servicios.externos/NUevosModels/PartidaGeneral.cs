using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class PartidaGeneral
    {
        public PartidaGeneral()
        {
            IndiceOcupacional = new HashSet<IndiceOcupacional>();
        }

        public int IdPartidaGeneral { get; set; }
        public string NumeroPartida { get; set; }

        public virtual ICollection<IndiceOcupacional> IndiceOcupacional { get; set; }
    }
}
