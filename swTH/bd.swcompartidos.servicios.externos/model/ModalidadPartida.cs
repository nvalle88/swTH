using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class ModalidadPartida
    {
        public ModalidadPartida()
        {
            IndiceOcupacional = new HashSet<IndiceOcupacional>();
        }

        public int IdModalidadPartida { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<IndiceOcupacional> IndiceOcupacional { get; set; }
    }
}
