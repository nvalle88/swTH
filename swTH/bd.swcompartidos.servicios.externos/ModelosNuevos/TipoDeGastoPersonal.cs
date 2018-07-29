using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class TipoDeGastoPersonal
    {
        public TipoDeGastoPersonal()
        {
            GastoPersonal = new HashSet<GastoPersonal>();
        }

        public int IdTipoGastoPersonal { get; set; }
        public string Descripcion { get; set; }
        public string NombreConstante { get; set; }

        public virtual ICollection<GastoPersonal> GastoPersonal { get; set; }
    }
}
