using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class EstadoTipoAccionPersonal
    {
        public EstadoTipoAccionPersonal()
        {
            TipoAccionPersonal = new HashSet<TipoAccionPersonal>();
        }

        public int IdEstadoTipoAccionPersonal { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<TipoAccionPersonal> TipoAccionPersonal { get; set; }
    }
}
